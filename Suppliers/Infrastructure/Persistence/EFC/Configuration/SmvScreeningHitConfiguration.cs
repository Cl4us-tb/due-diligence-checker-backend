using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class SmvScreeningHitConfiguration : IEntityTypeConfiguration<SmvScreeningHit>
{
    public void Configure(EntityTypeBuilder<SmvScreeningHit> builder)
    {
        builder.ToTable("smv_screening_hits", "suppliers");
        builder.HasKey(h => h.SmvScreeningHitId);

        builder.Property(h => h.Date).IsRequired();
        builder.Property(h => h.Resolution).IsRequired();
        builder.Property(h => h.Summary).IsRequired();
        builder.Property(h => h.Type).IsRequired();
        builder.Property(h => h.Amount).IsRequired();
        builder.Property(h => h.WithAppeal).IsRequired();
        builder.Property(h => h.ResolutiveResolutionNumber).IsRequired();
        builder.Property(h => h.ResolutiveResolutionDate).IsRequired();
    }
}
