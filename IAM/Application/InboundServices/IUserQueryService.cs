using DueDiligenceChecker.IAM.Domain.Model.Entities;
using DueDiligenceChecker.IAM.Domain.Model.Queries;

namespace DueDiligenceChecker.IAM.Application.InboundServices;

public interface IUserQueryService
{
    Task<(User user, string token)?> AuthenticateAsync(SignInQuery query);
}

