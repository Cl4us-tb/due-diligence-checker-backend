namespace DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;

using Microsoft.EntityFrameworkCore;
using DueDiligenceChecker.IAM.Domain.Model.Entities;
using DueDiligenceChecker.IAM.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
