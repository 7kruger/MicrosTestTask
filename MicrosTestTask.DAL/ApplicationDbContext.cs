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
	public DbSet<Profile> Profiles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(builder =>
		{
			builder.ToTable("Users").HasKey(x => x.Id);

			builder.HasData(new User
			{
				Id = 1,
				Name = "admin",
				Password = "a03ab19b866fc585b5cb1812a2f63ca861e7e7643ee5d43fd7106b623725fd67", // захешированный пароль '123'
				Role = Role.Admin,
				RegistrationDate = DateTime.Now,
				IsBlocked = false
			});
            builder.HasData(new User
            {
                Id = 2,
                Name = "kruger",
                Password = "a03ab19b866fc585b5cb1812a2f63ca861e7e7643ee5d43fd7106b623725fd67", // захешированный пароль '123'
                Role = Role.User,
                RegistrationDate = DateTime.Now,
                IsBlocked = false
            });

			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Password).IsRequired();
			builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

			builder.HasOne(x => x.Profile)
				.WithOne(x => x.User)
				.HasPrincipalKey<User>(x => x.Id)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Profile>(builder =>
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

			builder.HasMany(x => x.Operations)
				.WithOne(x => x.Category);
        });

		modelBuilder.Entity<Operation>(builder =>
		{
			builder.HasOne(x => x.Category)
				.WithMany(x => x.Operations);
		});
	}
}
