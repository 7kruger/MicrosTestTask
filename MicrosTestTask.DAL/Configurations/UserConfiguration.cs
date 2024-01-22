using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.Domain.Enums;
using MicrosTestTask.Domain.Helpers;

namespace MicrosTestTask.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.HasOne(x => x.Profile)
            .WithOne(x => x.User)
            .HasPrincipalKey<User>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new User
        {
            Id = 1,
            Name = "admin",
            Password = PasswordHashHelper.HashPassword("123"),
            Role = Role.Admin,
            RegistrationDate = DateTime.Now,
            IsBlocked = false
        });
        builder.HasData(new User
        {
            Id = 2,
            Name = "kruger",
            Password = PasswordHashHelper.HashPassword("123"),
            Role = Role.User,
            RegistrationDate = DateTime.Now,
            IsBlocked = false
        });
    }
}
