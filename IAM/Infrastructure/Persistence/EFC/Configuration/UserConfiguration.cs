using DueDiligenceChecker.IAM.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DueDiligenceChecker.IAM.Infrastructure.Persistence.EFC.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("users", "iam");
        
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.UserId).IsRequired().ValueGeneratedOnAdd();
        builder.Property(u => u.Fullname).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(100); 

        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired(false);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
