using Microsoft.EntityFrameworkCore;
using MicrosTestTask.DAL.Entities;
using MicrosTestTask.DAL.Enums;

namespace MicrosTestTask.DAL;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
		: base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<Operation> Operations { get; set; }
	public DbSet<Category> Categories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(builder =>
		{
			builder.HasData(new User
			{
				Id = 1,
				Name = "admin",
				Password = "a03ab19b866fc585b5cb1812a2f63ca861e7e7643ee5d43fd7106b623725fd67", // захешированный пароль '123'
				Role = Role.Admin,
				RegistrationDate = DateTime.Now,
				IsBlocked = false
			});
		});

		modelBuilder.Entity<Category>(builder =>
		{
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
        });
	}
}
