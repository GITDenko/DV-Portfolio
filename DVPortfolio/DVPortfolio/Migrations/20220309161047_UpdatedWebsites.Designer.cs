﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DVPortfolio.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220309161047_UpdatedWebsites")]
    partial class UpdatedWebsites
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.MainCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MainCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Hidden = false,
                            ImageURL = "photography.png",
                            Name = "Photography"
                        },
                        new
                        {
                            Id = 2,
                            Hidden = false,
                            ImageURL = "videos.png",
                            Name = "Videos"
                        },
                        new
                        {
                            Id = 3,
                            Hidden = false,
                            ImageURL = "websites.png",
                            Name = "Websites"
                        });
                });

            modelBuilder.Entity("Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<int?>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubcategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Photo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 3, 9, 17, 10, 46, 825, DateTimeKind.Local).AddTicks(8040),
                            Hidden = false,
                            ProductUrl = "berlin.png",
                            SubcategoryId = 1
                        });
                });

            modelBuilder.Entity("Entities.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("Subcategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Hidden = false,
                            ImageURL = "berlin.png",
                            MainCategoryId = 1,
                            Name = "Berlin"
                        });
                });

            modelBuilder.Entity("Entities.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<int?>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubcategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Videos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 3, 9, 17, 10, 46, 826, DateTimeKind.Local).AddTicks(3655),
                            Hidden = false,
                            MainCategoryId = 2,
                            ProductUrl = "https://www.youtube.com/watch?v=izGwDsrQ1eQ"
                        });
                });

            modelBuilder.Entity("Entities.Website", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubcategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Website");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 3, 9, 17, 10, 46, 826, DateTimeKind.Local).AddTicks(9382),
                            Hidden = false,
                            ImageURL = "google.jpg",
                            MainCategoryId = 3,
                            Name = "Google",
                            ProductUrl = "https://www.google.com"
                        });
                });

            modelBuilder.Entity("Entities.Photo", b =>
                {
                    b.HasOne("Entities.MainCategory", "MainCategory")
                        .WithMany("Photos")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Entities.Subcategory", "Subcategory")
                        .WithMany("Photos")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("MainCategory");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Entities.Subcategory", b =>
                {
                    b.HasOne("Entities.MainCategory", "MainCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("MainCategory");
                });

            modelBuilder.Entity("Entities.Video", b =>
                {
                    b.HasOne("Entities.MainCategory", "MainCategory")
                        .WithMany("Videos")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Entities.Subcategory", "Subcategory")
                        .WithMany("Videos")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("MainCategory");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Entities.Website", b =>
                {
                    b.HasOne("Entities.MainCategory", "MainCategory")
                        .WithMany("Websites")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.HasOne("Entities.Subcategory", "Subcategory")
                        .WithMany("Websites")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("MainCategory");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Entities.MainCategory", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("Subcategories");

                    b.Navigation("Videos");

                    b.Navigation("Websites");
                });

            modelBuilder.Entity("Entities.Subcategory", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("Videos");

                    b.Navigation("Websites");
                });
#pragma warning restore 612, 618
        }
    }
}
