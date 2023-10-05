using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));
        
        builder.HasKey(role => role.Id);
        builder.Property(role => role.Id).ValueGeneratedOnAdd();

        builder.Property(role => role.Title).HasMaxLength(32);
    }
}