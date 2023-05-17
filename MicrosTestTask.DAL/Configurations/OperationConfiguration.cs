using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicrosTestTask.DAL.Entities;

namespace MicrosTestTask.DAL.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.ToTable("Operations").HasKey(x => x.Id);

        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Sum).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.Category)
                .WithMany(x => x.Operations);
    }
}
