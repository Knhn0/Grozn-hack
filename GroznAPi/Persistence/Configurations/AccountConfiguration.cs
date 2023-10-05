using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(nameof(Account));

        builder.HasKey(account => account.Id);
        builder.Property(account => account.Id).ValueGeneratedOnAdd();
        builder.Property(account => account.Username).HasMaxLength(30);
        builder.Property(account => account.Password).HasMaxLength(30);
        builder.HasOne(account => account.UserInfo)
            .WithOne()
            .HasForeignKey<UserInfo>(info => info.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}