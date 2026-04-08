using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Infrastructure.Scraping;

public class SecopScraper : ISecopScraper
{
    private readonly HttpClient _httpClient;

    public SecopScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<SecopSanction>> SearchSanctionsAsync(
        SecopSanctionsByContractorNameQuery query,
        CancellationToken cancellationToken = default)
    {
        var datasetId = "4n4q-k399";
        var contractor = query.ContractorName ?? string.Empty;

        var filter = $"upper(nombre_contratista) like '%{contractor.ToUpperInvariant()}%'";
        var url = $"https://www.datos.gov.co/resource/{datasetId}.json?$where={Uri.EscapeDataString(filter)}";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.TryAddWithoutValidation("User-Agent", "DueDiligenceChecker-App");

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var rawItems = JsonSerializer.Deserialize<List<SecopRawItem>>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<SecopRawItem>();

        return rawItems.Select(Map).ToList();
    }

    private static SecopSanction Map(SecopRawItem item)
    {
        return new SecopSanction(
            item.EntityName ?? string.Empty,
            item.EntityTaxId ?? string.Empty,
            item.Level ?? string.Empty,
            item.Order ?? string.Empty,
            item.Municipality ?? string.Empty,
            item.ResolutionNumber ?? string.Empty,
            item.ContractorDocument ?? string.Empty,
            item.ContractorName ?? string.Empty,
            item.ContractNumber ?? string.Empty,
            ParseAmount(item.SanctionAmount),
            ParseDateTime(item.PublishedAt),
            ParseDateTime(item.FinalizedAt),
            ParseDateTime(item.LoadedAt),
            item.ProcessUrl ?? string.Empty);
    }

    private static decimal? ParseAmount(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;

        var normalized = new string(raw.Where(c => char.IsDigit(c) || c is '.' or ',').ToArray());
        if (string.IsNullOrWhiteSpace(normalized)) return null;

        normalized = normalized.Replace(".", string.Empty).Replace(",", string.Empty);

        return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out var value)
            ? value
            : null;
    }

    private static DateTime? ParseDateTime(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;
        return DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var value)
            ? value
            : null;
    }

    private sealed class SecopRawItem
    {
        [JsonPropertyName("nombre_entidad")]
        public string? EntityName { get; init; }

        [JsonPropertyName("nit_entidad")]
        public string? EntityTaxId { get; init; }

        [JsonPropertyName("nivel")]
        public string? Level { get; init; }

        [JsonPropertyName("orden")]
        public string? Order { get; init; }

        [JsonPropertyName("municipio")]
        public string? Municipality { get; init; }

        [JsonPropertyName("numero_de_resolucion")]
        public string? ResolutionNumber { get; init; }

        [JsonPropertyName("documento_contratista")]
        public string? ContractorDocument { get; init; }

        [JsonPropertyName("nombre_contratista")]
        public string? ContractorName { get; init; }

        [JsonPropertyName("numero_de_contrato")]
        public string? ContractNumber { get; init; }

        [JsonPropertyName("valor_sancion")]
        public string? SanctionAmount { get; init; }

        [JsonPropertyName("fecha_de_publicacion")]
        public string? PublishedAt { get; init; }

        [JsonPropertyName("fecha_de_firmeza")]
        public string? FinalizedAt { get; init; }

        [JsonPropertyName("fecha_de_cargue")]
        public string? LoadedAt { get; init; }

        [JsonPropertyName("ruta_de_proceso")]
        public string? ProcessUrl { get; init; }
    }
}
