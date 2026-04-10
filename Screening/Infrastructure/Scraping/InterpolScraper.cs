using System.Globalization;
using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;
using Microsoft.Playwright;

namespace DueDiligenceChecker.Screening.Infrastructure.Scraping;

public class InterpolScraper : IInterpolScraper
{
    public async Task<IReadOnlyList<InterpolRedNotice>> SearchRedNoticesAsync(
        InterpolRedNoticesQuery query,
        CancellationToken cancellationToken = default)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            Args = new[] { "--disable-blink-features=AutomationControlled" }
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36",
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        var page = await context.NewPageAsync();
        page.SetDefaultTimeout(60000);

        try
        {
            await page.GotoAsync(
                "https://www.interpol.int/How-we-work/Notices/Red-Notices/View-Red-Notices",
                new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

            await page.Locator("#name").FillAsync(query.FamilyName ?? string.Empty);
            await page.Locator("#forename").FillAsync(query.Forename ?? string.Empty);

            if (!string.IsNullOrWhiteSpace(query.Nationality))
            {
                await page.Locator("#nationality").ClickAsync();
                await page.Locator("#nationality").PressSequentiallyAsync(query.Nationality, new LocatorPressSequentiallyOptions { Delay = 100 });
                await page.WaitForTimeoutAsync(1000);
                await page.Keyboard.PressAsync("Enter");
                await page.Keyboard.PressAsync("Tab");
            }

            if (query.Age.HasValue)
            {
                await page.Locator("#ageMin").FillAsync(query.Age.Value.ToString(CultureInfo.InvariantCulture));
                await page.Locator("#ageMax").FillAsync(query.Age.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (!string.IsNullOrWhiteSpace(query.Gender))
            {
                var genderId = query.Gender.Trim().ToUpperInvariant() switch
                {
                    "MALE" or "M" => "sexId_1",
                    "FEMALE" or "F" => "sexId_0",
                    "UNKNOWN" or "U" => "sexId_2",
                    _ => null
                };

                if (genderId != null)
                {
                    await page.Locator($"label[for='{genderId}']").ClickAsync();
                }
            }

            var resultsCountLocator = page.Locator("#searchResults");
            await resultsCountLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            var initialResultsCountText = (await resultsCountLocator.InnerTextAsync()).Trim();

            await page.Locator("#submit").ClickAsync();

            await page.WaitForFunctionAsync(
                "(initial) => { const el = document.getElementById('searchResults'); if (!el) return false; const t = el.innerText.trim(); return t !== (initial || '').trim(); }",
                initialResultsCountText,
                new PageWaitForFunctionOptions { Timeout = 60000 });

            var resultsCountText = (await resultsCountLocator.InnerTextAsync()).Trim();
            var digitsOnly = new string(resultsCountText.Where(char.IsDigit).ToArray());
            if (!int.TryParse(digitsOnly, out var totalHits) || totalHits == 0)
                return Array.Empty<InterpolRedNotice>();

            var firstResult = page.Locator(".redNoticeItem__labelLink").First;
            await firstResult.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await firstResult.ClickAsync();

            var detailPanel = page.Locator("#singlePanel");
            await detailPanel.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            var familyName = (await detailPanel.Locator("#name").InnerTextAsync()).Trim();
            var forename = (await detailPanel.Locator("#forename").InnerTextAsync()).Trim();
            var gender = (await detailPanel.Locator("#sex_id").InnerTextAsync()).Trim();
            var dateOfBirthRaw = (await detailPanel.Locator("#date_of_birth").InnerTextAsync()).Trim();
            var placeOfBirth = (await detailPanel.Locator("#place_of_birth").InnerTextAsync()).Trim();
            var nationality = (await detailPanel.Locator("#nationalities").InnerTextAsync()).Trim();
            var charges = (await detailPanel.Locator("#charge").InnerTextAsync()).Trim();

            var dateOfBirth = ParseDateOnly(dateOfBirthRaw);

            return new[]
            {
                new InterpolRedNotice(
                    familyName,
                    forename,
                    gender,
                    dateOfBirth,
                    placeOfBirth,
                    nationality,
                    charges)
            };
        }
        finally
        {
            await browser.CloseAsync();
        }
    }

    private static DateOnly? ParseDateOnly(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;
        return DateOnly.TryParseExact(raw, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var value)
            ? value
            : null;
    }
}
