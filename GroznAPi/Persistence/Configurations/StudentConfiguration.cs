using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(nameof(Student));
        builder.HasKey(student => student.Id);
        builder.Property(student => student.Id).ValueGeneratedOnAdd();

        builder.HasOne(student => student.User)
            .WithOne()
            .HasForeignKey<UserInfo>(info => info.Id);


        builder.OwnsMany(student => student.Courses)
            .WithOwner()
            .HasForeignKey(course => course.Id);
            
        builder.OwnsMany(student => student.StudentTestPercents)
            .WithOwner()
            .HasForeignKey(course => course.StudentId);
    }
}