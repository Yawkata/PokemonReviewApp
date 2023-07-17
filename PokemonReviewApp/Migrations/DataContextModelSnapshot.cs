﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonReviewApp.Data;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PokemonReviewApp.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Electric"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Water"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Leaf"
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Something"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Anything"
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gym")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 1,
                            FirstName = "Gosho",
                            Gym = "Gymski",
                            LastName = "Toshev",
                            Nickname = "Admin",
                            Password = "admin",
                            Role = "Administrator"
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1903, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pikachu"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1903, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Squirtle"
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(1903, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Venasuar"
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.PokemonCategory", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("PokemonId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("PokemonCategories");

                    b.HasData(
                        new
                        {
                            PokemonId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            PokemonId = 2,
                            CategoryId = 2
                        },
                        new
                        {
                            PokemonId = 3,
                            CategoryId = 3
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.PokemonOwner", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("PokemonId", "OwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("PokemonOwners");

                    b.HasData(
                        new
                        {
                            PokemonId = 1,
                            OwnerId = 1
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PokemonId = 1,
                            Rating = 5,
                            ReviewerId = 1,
                            Text = "Pickahu is the best Pokémon because it is electric",
                            Title = "Pikachu"
                        },
                        new
                        {
                            Id = 2,
                            PokemonId = 1,
                            Rating = 5,
                            ReviewerId = 2,
                            Text = "Pickachu is the best at killing rocks",
                            Title = "Pikachu"
                        },
                        new
                        {
                            Id = 3,
                            PokemonId = 1,
                            Rating = 1,
                            ReviewerId = 3,
                            Text = "Pickchu, pickachu, pikachu",
                            Title = "Pikachu"
                        },
                        new
                        {
                            Id = 4,
                            PokemonId = 2,
                            Rating = 5,
                            ReviewerId = 1,
                            Text = "Squirtle is the best Pokémon because it is electric",
                            Title = "Squirtle"
                        },
                        new
                        {
                            Id = 5,
                            PokemonId = 2,
                            Rating = 5,
                            ReviewerId = 2,
                            Text = "Squirtle is the best at killing rocks",
                            Title = "Squirtle"
                        },
                        new
                        {
                            Id = 6,
                            PokemonId = 2,
                            Rating = 1,
                            ReviewerId = 3,
                            Text = "Squirtle, squirtle, squirtle",
                            Title = "Squirtle"
                        },
                        new
                        {
                            Id = 7,
                            PokemonId = 3,
                            Rating = 5,
                            ReviewerId = 1,
                            Text = "Venasaur is the best Pokémon because it is electric",
                            Title = "Venasaur"
                        },
                        new
                        {
                            Id = 8,
                            PokemonId = 3,
                            Rating = 5,
                            ReviewerId = 2,
                            Text = "Venasaur is the best at killing rocks",
                            Title = "Venasaur"
                        },
                        new
                        {
                            Id = 9,
                            PokemonId = 3,
                            Rating = 1,
                            ReviewerId = 3,
                            Text = "Venasaur, Venasaur, Venasaur",
                            Title = "Venasaur"
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Reviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviewers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Teddy",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Taylor",
                            LastName = "Jones"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Jessica",
                            LastName = "McGregor"
                        });
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Owner", b =>
                {
                    b.HasOne("PokemonReviewApp.Models.Country", "Country")
                        .WithMany("Owners")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.PokemonCategory", b =>
                {
                    b.HasOne("PokemonReviewApp.Models.Category", "Category")
                        .WithMany("PokemonCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonReviewApp.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonCategories")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.PokemonOwner", b =>
                {
                    b.HasOne("PokemonReviewApp.Models.Owner", "Owner")
                        .WithMany("PokemonOwners")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonReviewApp.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonOwners")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Review", b =>
                {
                    b.HasOne("PokemonReviewApp.Models.Pokemon", "Pokemon")
                        .WithMany("Reviews")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonReviewApp.Models.Reviewer", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pokemon");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Category", b =>
                {
                    b.Navigation("PokemonCategories");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Country", b =>
                {
                    b.Navigation("Owners");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Owner", b =>
                {
                    b.Navigation("PokemonOwners");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Pokemon", b =>
                {
                    b.Navigation("PokemonCategories");

                    b.Navigation("PokemonOwners");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PokemonReviewApp.Models.Reviewer", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
