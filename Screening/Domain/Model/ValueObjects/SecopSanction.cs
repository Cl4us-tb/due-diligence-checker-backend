namespace DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

public record SecopSanction(
    string EntityName,
    string EntityTaxId,
    string Level,
    string Order,
    string Municipality,
    string ResolutionNumber,
    string ContractorDocument,
    string ContractorName,
    string ContractNumber,
    decimal? SanctionAmount,
    DateTime? PublishedAt,
    DateTime? FinalizedAt,
    DateTime? LoadedAt,
    string ProcessUrl);
