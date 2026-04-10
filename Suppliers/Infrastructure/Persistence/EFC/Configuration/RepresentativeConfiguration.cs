using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class RepresentativeConfiguration : IEntityTypeConfiguration<Representative>
{
    public void Configure(EntityTypeBuilder<Representative> builder)
    {
        builder.ToTable("representatives", "suppliers");

        builder.HasKey(r => r.RepresentativeId);

        builder.Property(r => r.Role).IsRequired();
        builder.Property(r => r.FirstName).IsRequired();
        builder.Property(r => r.LastName).IsRequired();
        builder.Property(r => r.Age);
        builder.Property(r => r.Nationality);
    }
}
