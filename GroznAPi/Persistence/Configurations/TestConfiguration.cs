using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.ToTable(nameof(Test));
        
        builder.HasKey(test => test.Id);
        builder.Property(test => test.Id).ValueGeneratedOnAdd();

        builder.Property(test => test.Title).HasMaxLength(60);
        builder.Property(test => test.Description).HasMaxLength(300);
    }
}