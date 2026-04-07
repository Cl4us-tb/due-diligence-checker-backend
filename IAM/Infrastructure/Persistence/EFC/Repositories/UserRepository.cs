using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;
using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Repositories;
using DueDiligenceChecker.IAM.Domain.Model.Entities;
using DueDiligenceChecker.IAM.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DueDiligenceChecker.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Context.Set<User>().AnyAsync(u => u.Email == email);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
    }
}
