using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable(nameof(Teacher));
        
        builder.HasKey(lesson => lesson.Id);
        builder.Property(lesson => lesson.Id).ValueGeneratedOnAdd();

        builder.HasOne(lesson => lesson.UserInfo)
            .WithOne()
            .HasForeignKey<UserInfo>(info => info.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}