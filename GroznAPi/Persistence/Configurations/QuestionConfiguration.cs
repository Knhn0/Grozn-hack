using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable(nameof(Question));

        builder.HasKey(question => question.Id);
        builder.Property(question => question.Id).ValueGeneratedOnAdd();

        builder.HasOne(question => question.Resource)
            .WithOne()
            .HasForeignKey<Resource>(resource => resource.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(question => question.Test)
            .WithOne()
            .HasForeignKey<Test>(test => test.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(question => question.Answers)
            .WithOwner()
            .HasForeignKey(answer => answer.QuestionId);
    }
}