﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApi.Database;

namespace WebApi.Migrations
{
    [DbContext(typeof(WebApiContext))]
    [Migration("20210916172231_Addresses")]
    partial class Addresses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Add")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("Postal")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            Add = "Tec Ballerup",
                            City = "Ballerup",
                            Postal = 2700,
                            UserId = 0
                        },
                        new
                        {
                            AddressId = 2,
                            Add = "Tec Ballerup",
                            City = "Ballerup",
                            Postal = 2700,
                            UserId = 0
                        });
                });

            modelBuilder.Entity("WebApi.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateTime")
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            DateTime = "Friday 13th at 4:00",
                            ItemId = 1,
                            UserId = 1
                        },
                        new
                        {
                            OrderId = 2,
                            DateTime = "Friday 13th at 4:00",
                            ItemId = 2,
                            UserId = 2
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
