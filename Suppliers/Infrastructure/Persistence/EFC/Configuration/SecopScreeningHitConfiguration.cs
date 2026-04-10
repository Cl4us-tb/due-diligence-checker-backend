using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class SecopScreeningHitConfiguration : IEntityTypeConfiguration<SecopScreeningHit>
{
    public void Configure(EntityTypeBuilder<SecopScreeningHit> builder)
    {
        builder.ToTable("secop_screening_hits", "suppliers");
        builder.HasKey(h => h.SecopScreeningHitId);

        builder.Property(h => h.EntityName).IsRequired();
        builder.Property(h => h.EntityTaxId).IsRequired();
        builder.Property(h => h.Level).IsRequired();
        builder.Property(h => h.Order).IsRequired();
        builder.Property(h => h.Municipality).IsRequired();
        builder.Property(h => h.ResolutionNumber).IsRequired();
        builder.Property(h => h.ContractorDocument).IsRequired();
        builder.Property(h => h.ContractorName).IsRequired();
        builder.Property(h => h.ContractNumber).IsRequired();
        builder.Property(h => h.SanctionAmount).HasPrecision(18, 2);
        builder.Property(h => h.PublishedAt);
        builder.Property(h => h.FinalizedAt);
        builder.Property(h => h.LoadedAt);
        builder.Property(h => h.ProcessUrl).IsRequired();
    }
}
