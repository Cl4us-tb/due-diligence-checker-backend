namespace DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;

using Microsoft.EntityFrameworkCore;
using DueDiligenceChecker.IAM.Domain.Model.Entities;
using DueDiligenceChecker.IAM.Infrastructure.Persistence.EFC.Configuration;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Representative> Representatives { get; set; }
    public DbSet<SupplierScreening> SupplierScreenings { get; set; }
    public DbSet<InterpolScreeningHit> InterpolScreeningHits { get; set; }
    public DbSet<SecopScreeningHit> SecopScreeningHits { get; set; }
    public DbSet<SmvScreeningHit> SmvScreeningHits { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
