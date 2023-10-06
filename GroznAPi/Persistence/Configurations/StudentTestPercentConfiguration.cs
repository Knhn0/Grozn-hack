using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class StudentTestPercentConfiguration : IEntityTypeConfiguration<StudentTestPercent>
{
    public void Configure(EntityTypeBuilder<StudentTestPercent> builder)
    {
        builder.ToTable(nameof(StudentTestPercent));
        
        builder.HasKey(percent => percent.Id);
        builder.Property(percent => percent.Id).ValueGeneratedOnAdd();

        builder.HasOne(percent => percent.Test)
            .WithOne()
            .HasForeignKey<Test>(test => test.Id);

        builder.HasOne(percent => percent.Student)
            .WithOne()
            .HasForeignKey<Student>(student => student.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}