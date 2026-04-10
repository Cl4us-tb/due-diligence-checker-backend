using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class InterpolScreeningHitConfiguration : IEntityTypeConfiguration<InterpolScreeningHit>
{
    public void Configure(EntityTypeBuilder<InterpolScreeningHit> builder)
    {
        builder.ToTable("interpol_screening_hits", "suppliers");
        builder.HasKey(h => h.InterpolScreeningHitId);

        builder.Property(h => h.FamilyName).IsRequired();
        builder.Property(h => h.Forename).IsRequired();
        builder.Property(h => h.Gender).IsRequired();
        builder.Property(h => h.DateOfBirth);
        builder.Property(h => h.PlaceOfBirth).IsRequired();
        builder.Property(h => h.Nationality).IsRequired();
        builder.Property(h => h.Charges).IsRequired();
    }
}
