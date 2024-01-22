using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicrosTestTask.DAL.Entities;

namespace MicrosTestTask.DAL.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles").HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ImgRef).IsRequired(false);

        builder.HasData(new Profile
        {
            Id = 1,
            ImgRef = "/images/person.svg",
            UserId = 1
        });
        builder.HasData(new Profile
        {
            Id = 2,
            ImgRef = "/images/person.svg",
            UserId = 2
        });
    }
}
