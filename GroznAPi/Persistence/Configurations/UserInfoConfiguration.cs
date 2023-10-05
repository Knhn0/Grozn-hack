using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.ToTable(nameof(UserInfo));
        builder.HasKey(k => k.Id);
        builder.Property(k => k.Id).ValueGeneratedOnAdd();
        builder.Property(k => k.FirstName).HasMaxLength(60);
        builder.Property(k => k.SecondName).HasMaxLength(60);
        builder.Property(k => k.ThirdName).HasMaxLength(60);
        builder.HasOne(userInfo => userInfo.Role)
            .WithOne()
            .HasForeignKey<Role>(role => role.Id);
    }
}