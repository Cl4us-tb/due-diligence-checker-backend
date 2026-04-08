using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;
using Microsoft.Playwright;

namespace DueDiligenceChecker.Screening.Infrastructure.Scraping;

public class SmvScraper : ISmvScraper
{
    public async Task<IReadOnlyList<SmvSanction>> SearchSanctionsAsync(
        SmvSanctionsByEntityNameQuery query,
        CancellationToken cancellationToken = default)
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        var page = await browser.NewPageAsync();
        page.SetDefaultTimeout(30000);

        try
        {
            await page.GotoAsync("https://www.smv.gob.pe/SIMV/Default.aspx", new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
            await page.Locator("#txtSearch").FillAsync(query.EntityName);
            await page.Locator("#ibtnB").ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var btnSanciones = page.Locator("#btnSanciones");
            if (await btnSanciones.CountAsync() == 0) return Array.Empty<SmvSanction>();

            await btnSanciones.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.EvaluateAsync("window.ValidarCampo = () => true;");
            await page.EvaluateAsync("document.getElementById('txtFechDesde').value = '01/01/2008';");
            await page.EvaluateAsync($"document.getElementById('txtFechHasta').value = '{DateTime.Today:dd/MM/yyyy}';");

            await page.Locator("#MainContent_cbBuscar").ClickAsync();
            await page.WaitForTimeoutAsync(3000);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var rows = await page.Locator("#MainContent_grdReporte tr:not(.headertable)").AllAsync();
            var results = new List<SmvSanction>();

            foreach (var row in rows)
            {
                var tds = await row.Locator("td").AllAsync();
                if (tds.Count < 9) continue;

                var date = (await tds[1].InnerTextAsync()).Trim();
                var resolution = await tds[2].Locator("[id*='HyperLink1']").CountAsync() > 0
                    ? (await tds[2].Locator("[id*='HyperLink1']").InnerTextAsync()).Trim()
                    : (await tds[2].InnerTextAsync()).Trim();
                var summary = await tds[3].Locator("[id*='lblLeyenda']").CountAsync() > 0
                    ? (await tds[3].Locator("[id*='lblLeyenda']").InnerTextAsync()).Trim()
                    : (await tds[3].InnerTextAsync()).Trim();
                var type = (await tds[4].InnerTextAsync()).Trim();
                var amount = await tds[5].Locator("[id*='lblMonto']").CountAsync() > 0
                    ? (await tds[5].Locator("[id*='lblMonto']").InnerTextAsync()).Trim()
                    : (await tds[5].InnerTextAsync()).Trim();
                var withAppeal = (await tds[6].InnerTextAsync()).Trim();
                var resolutiveResolutionNumber = await tds[7].Locator("[id*='HyperLink2']").CountAsync() > 0
                    ? (await tds[7].Locator("[id*='HyperLink2']").InnerTextAsync()).Trim()
                    : (await tds[7].InnerTextAsync()).Trim();
                var resolutiveResolutionDate = (await tds[8].InnerTextAsync()).Trim();

                results.Add(new SmvSanction(
                    date,
                    resolution,
                    summary,
                    type,
                    amount,
                    withAppeal,
                    resolutiveResolutionNumber,
                    resolutiveResolutionDate));
            }

            return results;
        }
        finally
        {
            await browser.CloseAsync();
        }
    }
}
