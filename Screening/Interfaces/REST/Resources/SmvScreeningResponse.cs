using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Interfaces.REST.Resources;

public record SmvScreeningResponse(int TotalHits, IReadOnlyList<SmvSanction> Items);
