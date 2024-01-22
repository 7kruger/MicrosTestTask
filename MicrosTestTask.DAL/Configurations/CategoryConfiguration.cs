using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.Domain.Enums;

namespace MicrosTestTask.DAL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CategoryType).IsRequired();

        builder.HasMany(x => x.Operations)
            .WithOne(x => x.Category);

        builder.HasData(new Category
        {
            Id = 1,
            Name = "Иные доходы",
            CategoryType = CategoryType.Income
        });
        builder.HasData(new Category
        {
            Id = 2,
            Name = "Другое",
            CategoryType = CategoryType.Expense
        });
        builder.HasData(new Category
        {
            Id = 3,
            Name = "Еда",
            CategoryType = CategoryType.Expense
        });
        builder.HasData(new Category
        {
            Id = 4,
            Name = "Интернет",
            CategoryType = CategoryType.Expense
        }); builder.HasData(new Category
        {
            Id = 5,
            Name = "Зарплата",
            CategoryType = CategoryType.Income
        });
        builder.HasData(new Category
        {
            Id = 6,
            Name = "Сдача квартиры в аренду",
            CategoryType = CategoryType.Income
        });
    }
}
