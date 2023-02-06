﻿// <auto-generated />
using System;
using MicrosTestTask.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MicrosTestTask.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230206192056_AddProfilesTable")]
    partial class AddProfilesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryType = 0,
                            Name = "Иные доходы"
                        },
                        new
                        {
                            Id = 2,
                            CategoryType = 1,
                            Name = "Другое"
                        });
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.Operation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Sum")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImgRef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImgRef = "/images/person.svg",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            ImgRef = "/images/person.svg",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsBlocked = false,
                            Name = "admin",
                            Password = "a03ab19b866fc585b5cb1812a2f63ca861e7e7643ee5d43fd7106b623725fd67",
                            RegistrationDate = new DateTime(2023, 2, 7, 0, 20, 55, 791, DateTimeKind.Local).AddTicks(121),
                            Role = 1
                        },
                        new
                        {
                            Id = 2,
                            IsBlocked = false,
                            Name = "kruger",
                            Password = "a03ab19b866fc585b5cb1812a2f63ca861e7e7643ee5d43fd7106b623725fd67",
                            RegistrationDate = new DateTime(2023, 2, 7, 0, 20, 55, 791, DateTimeKind.Local).AddTicks(146),
                            Role = 0
                        });
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.Operation", b =>
                {
                    b.HasOne("MicrosTestTask.DAL.Entities.Category", "Category")
                        .WithMany("Operations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicrosTestTask.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.Profile", b =>
                {
                    b.HasOne("MicrosTestTask.DAL.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("MicrosTestTask.DAL.Entities.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.Category", b =>
                {
                    b.Navigation("Operations");
                });

            modelBuilder.Entity("MicrosTestTask.DAL.Entities.User", b =>
                {
                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
