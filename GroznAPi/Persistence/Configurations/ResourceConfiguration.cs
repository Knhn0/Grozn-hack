using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(nameof(Resource));

        builder.HasKey(resource => resource.Id);
        builder.Property(resource => resource.Id).ValueGeneratedOnAdd();

        builder.Property(resource => resource.Name).HasMaxLength(60);
        builder.Property(resource => resource.Type).HasMaxLength(60);
        builder.Property(resource => resource.Url);
    }
}