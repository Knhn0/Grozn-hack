using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(nameof(Course));

        builder.HasKey(course => course.Id);
        builder.Property(course => course.Id).ValueGeneratedOnAdd();

        builder.Property(course => course.Title).HasMaxLength(100);
        builder.Property(course => course.Description).HasMaxLength(300);

        builder.HasOne(course => course.Teacher)
            .WithOne()
            .HasForeignKey<Teacher>(teacher => teacher.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(course => course.Students)
            .WithMany(x => x.Courses);

        builder.HasMany(course => course.Themes)
            .WithOne()
            .HasForeignKey(theme => theme.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}