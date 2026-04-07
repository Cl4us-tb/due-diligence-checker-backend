using DueDiligenceChecker.IAM.Domain.Model.Entities;

namespace DueDiligenceChecker.IAM.Application.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
}
