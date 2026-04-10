using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Interfaces.REST.Resources;

namespace DueDiligenceChecker.Screening.Interfaces.REST.Transform;

public static class ScreeningQueryFromRequestAssembler
{
    public static SmvSanctionsByEntityNameQuery ToQuery(SmvScreeningRequest request)
        => new(request.EntityName);

    public static SecopSanctionsByContractorNameQuery ToQuery(SecopScreeningRequest request)
        => new(request.ContractorName);

    public static InterpolRedNoticesQuery ToQuery(InterpolRedNoticeScreeningRequest request)
        => new(request.FamilyName, request.Forename, request.Nationality, request.Age, request.Gender);
}
