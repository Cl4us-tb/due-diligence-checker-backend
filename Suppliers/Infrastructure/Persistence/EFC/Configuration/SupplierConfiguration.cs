using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Configuration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers", "suppliers");

        builder.HasKey(s => s.SupplierId);

        builder.OwnsOne(s => s.TaxId, taxId =>
        {
            taxId.Property(t => t.Value).HasColumnName("tax_identification").IsRequired();
        });

        builder.OwnsOne(s => s.Billing, billing =>
        {
            billing.Property(b => b.Amount).HasColumnName("annual_billing_amount").HasPrecision(18, 2).IsRequired();
        });

        builder.HasMany(s => s.Representatives)
            .WithOne()
            .HasForeignKey("SupplierId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.CorporateName).IsRequired();
        builder.Property(s => s.TradeName).IsRequired();
        builder.Property(s => s.PhoneNumber).IsRequired();
        builder.Property(s => s.Email).IsRequired();
        builder.Property(s => s.WebSite).IsRequired();
        builder.Property(s => s.PhysicalAddress).IsRequired();
        builder.Property(s => s.Country).IsRequired();

        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt);
        builder.Property(s => s.CreatedBy).IsRequired();
    }
}
