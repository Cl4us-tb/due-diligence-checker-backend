using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Interfaces.REST.Resources;

public record InterpolScreeningResponse(int TotalHits, IReadOnlyList<InterpolRedNotice> Items);
