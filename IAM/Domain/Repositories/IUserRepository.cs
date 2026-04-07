using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.IAM.Domain.Model.Entities;

namespace DueDiligenceChecker.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> FindByEmailAsync(string email);
}
