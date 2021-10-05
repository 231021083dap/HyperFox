﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Database;

namespace WebApi.Migrations
{
    [DbContext(typeof(WebApiContext))]
    [Migration("20210924063329_DTest")]
    partial class DTest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Database.Entities.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilmName")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<short>("RuntimeInMin")
                        .HasColumnType("smallInt");

                    b.Property<short>("Stock")
                        .HasColumnType("smallInt");

                    b.HasKey("FilmId");

                    b.HasIndex("GenreId");

                    b.ToTable("Film");

                    b.HasData(
                        new
                        {
                            FilmId = 1,
                            Description = "This movie is about a ring",
                            FilmName = "The lord of the rings",
                            GenreId = 1,
                            Image = "C:\\Users\\Tec\\Pictures\\1.jpg",
                            Price = 79.99m,
                            ReleaseDate = "16-09-2001",
                            RuntimeInMin = (short)123,
                            Stock = (short)50
                        },
                        new
                        {
                            FilmId = 2,
                            Description = "This movie is about the wizard world",
                            FilmName = "Harry potter",
                            GenreId = 1,
                            Image = "C:\\Users\\Tec\\Pictures\\2.jpg",
                            Price = 79.99m,
                            ReleaseDate = "16-09-2001",
                            RuntimeInMin = (short)123,
                            Stock = (short)50
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            GenreName = "Comedy"
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("Postal")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            City = "Ballerup",
                            Postal = 2700,
                            StreetName = "Tec Ballerup",
                            UserId = 1
                        },
                        new
                        {
                            AddressId = 2,
                            City = "Kattegat",
                            Postal = 2700,
                            StreetName = "Havet",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("FilmId");

                    b.HasIndex("OrderId");

                    b.ToTable("Item");

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            FilmId = 1,
                            OrderId = 1,
                            Price = 1,
                            Quantity = 1
                        },
                        new
                        {
                            ItemId = 2,
                            FilmId = 2,
                            OrderId = 2,
                            Price = 2,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            DateTime = "2001-08-21 04:45:21",
                            UserId = 1
                        },
                        new
                        {
                            OrderId = 2,
                            DateTime = "2001-08-21 04:45:41",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("WebApi.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Admin")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Admin = "Admin",
                            Email = "TestMail",
                            Password = "TestPassword",
                            UserName = "TestUserName"
                        },
                        new
                        {
                            UserId = 2,
                            Admin = "User",
                            Email = "Test2",
                            Password = "Test2",
                            UserName = "Test2"
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Film", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Genre", "Genre")
                        .WithMany("Films")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("WebApi.Entities.Address", b =>
                {
                    b.HasOne("WebApi.Entities.User", "User")
                        .WithOne("Addresses")
                        .HasForeignKey("WebApi.Entities.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Entities.Item", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Entities.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("WebApi.Entities.Order", b =>
                {
                    b.HasOne("WebApi.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Genre", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("WebApi.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("WebApi.Entities.User", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
