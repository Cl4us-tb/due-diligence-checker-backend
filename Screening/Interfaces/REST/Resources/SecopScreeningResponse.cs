using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Interfaces.REST.Resources;

public record SecopScreeningResponse(int TotalHits, IReadOnlyList<SecopSanction> Items);
