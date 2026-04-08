using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class SupplierScreeningConfiguration : IEntityTypeConfiguration<SupplierScreening>
{
    public void Configure(EntityTypeBuilder<SupplierScreening> builder)
    {
        builder.ToTable("supplier_screenings", "suppliers");

        builder.HasKey(s => s.SupplierScreeningId);

        builder.Property(s => s.ExecutedAt).IsRequired();
        builder.Property(s => s.HasHits).IsRequired();
        builder.Property(s => s.SourcesChecked).IsRequired();

        builder.HasOne<Supplier>()
            .WithMany()
            .HasForeignKey(s => s.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.InterpolHits)
            .WithOne()
            .HasForeignKey("SupplierScreeningId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.SecopHits)
            .WithOne()
            .HasForeignKey("SupplierScreeningId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.SmvHits)
            .WithOne()
            .HasForeignKey("SupplierScreeningId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
