using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> builder)
    {
        builder.ToTable(nameof(Theme));
        
        builder.HasKey(theme => theme.Id);
        builder.Property(theme => theme.Id).ValueGeneratedOnAdd();
        
        builder.Property(theme => theme.Title).HasMaxLength(60);
        builder.Property(theme => theme.Description).HasMaxLength(300);
        builder.HasMany<Lesson>(theme => theme.Lessons);
    }
}