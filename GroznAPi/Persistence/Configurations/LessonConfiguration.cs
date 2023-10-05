using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable(nameof(Lesson));
        
        builder.HasKey(lesson => lesson.Id);
        builder.Property(lesson => lesson.Id).ValueGeneratedOnAdd();

        builder.Property(lesson => lesson.Title).HasMaxLength(60);

        builder.HasOne(lesson => lesson.Theme)
            .WithOne()
            .HasForeignKey<Theme>(theme => theme.Id);
    }
}