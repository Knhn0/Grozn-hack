using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable(nameof(Answer));

        builder.HasKey(answer => answer.Id);
        builder.Property(answer => answer.Id).ValueGeneratedOnAdd();

        builder.Property(answer => answer.Title).HasMaxLength(512);
    }
}