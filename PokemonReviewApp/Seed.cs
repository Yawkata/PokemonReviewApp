using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp
{
    public static class Seed
    {
        public static void Seeder(this ModelBuilder db)
        {
            var country = new List<Country>()
            {
                new Country
                {
                    Id = 1,
                    Name = "Something"
                },
                new Country
                {
                    Id = 2,
                    Name = "Anything"
                }
            };

            db.Entity<Country>().HasData(country);

            var owners = new List<Owner>()
            {
                new Owner
                {
                    Id = 1,
                    Nickname = "Admin",
                    Password = "admin",
                    FirstName = "Gosho",
                    LastName = "Toshev",
                    CountryId = 1,
                    Gym = "Gymski"
                }
            };

            db.Entity<Owner>().HasData(owners);

            var pokemon = new List<Pokemon>
            {
                new Pokemon
                {
                    Id = 1,
                    Name = "Pikachu",
                    BirthDate = new DateTime(1903, 1, 1)
                },
                new Pokemon
                {
                    Id = 2,
                    Name = "Squirtle",
                    BirthDate = new DateTime(1903, 1, 1)
                },
                new Pokemon
                {
                    Id = 3,
                    Name = "Venasuar",
                    BirthDate = new DateTime(1903, 1, 1)
                }
            };

            db.Entity<Pokemon>().HasData(pokemon);

            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Electric"
                },
                new Category
                {
                    Id = 2,
                    Name = "Water"
                },
                new Category
                {
                    Id = 3,
                    Name = "Leaf"
                }
            };

            db.Entity<Category>().HasData(categories);

            var pokemonCategories = new List<PokemonCategory>
            {
                new PokemonCategory
                {
                    PokemonId = 1,
                    CategoryId = 1
                },
                new PokemonCategory
                {
                    PokemonId = 2,
                    CategoryId = 2
                },
                new PokemonCategory
                {
                    PokemonId = 3,
                    CategoryId = 3
                }
            };

            db.Entity<PokemonCategory>().HasData(pokemonCategories);

            var reviewers = new List<Reviewer>
            {
                new Reviewer
                {
                    Id = 1,
                    FirstName = "Teddy",
                    LastName = "Smith"
                },
                new Reviewer
                {
                    Id = 2,
                    FirstName = "Taylor",
                    LastName = "Jones"
                },
                new Reviewer
                {
                    Id = 3,
                    FirstName = "Jessica",
                    LastName = "McGregor"
                }
            };

            db.Entity<Reviewer>().HasData(reviewers);

            var pokeOwners = new List<PokemonOwner>
            {
                new PokemonOwner
                {
                    OwnerId = 1,
                    PokemonId = 1
                }
            };

            db.Entity<PokemonOwner>().HasData(pokeOwners);

            var reviews = new List<Review>
            {
                new Review
                {
                    Id = 1,
                    Title = "Pikachu",
                    Text = "Pickahu is the best Pokémon because it is electric",
                    Rating = 5,
                    PokemonId = 1,
                    ReviewerId = 1
                },
                new Review
                {
                    Id = 2,
                    Title = "Pikachu",
                    Text = "Pickachu is the best at killing rocks",
                    Rating = 5,
                    PokemonId = 1,
                    ReviewerId = 2
                },
                new Review
                {
                    Id = 3,
                    Title = "Pikachu",
                    Text = "Pickchu, pickachu, pikachu",
                    Rating = 1,
                    PokemonId = 1,
                    ReviewerId = 3
                },
                new Review
                {
                    Id = 4,
                    Title = "Squirtle",
                    Text = "Squirtle is the best Pokémon because it is electric",
                    Rating = 5,
                    PokemonId = 2,
                    ReviewerId = 1
                },
                new Review
                {
                    Id = 5,
                    Title = "Squirtle",
                    Text = "Squirtle is the best at killing rocks",
                    Rating = 5,
                    PokemonId = 2,
                    ReviewerId = 2
                },
                new Review
                {
                    Id = 6,
                    Title = "Squirtle",
                    Text = "Squirtle, squirtle, squirtle",
                    Rating = 1,
                    PokemonId = 2,
                    ReviewerId = 3
                },
                new Review
                {
                    Id = 7,
                    Title = "Venasaur",
                    Text = "Venasaur is the best Pokémon because it is electric",
                    Rating = 5,
                    PokemonId = 3,
                    ReviewerId = 1
                },
                new Review
                {
                    Id = 8,
                    Title = "Venasaur",
                    Text = "Venasaur is the best at killing rocks",
                    Rating = 5,
                    PokemonId = 3,
                    ReviewerId = 2
                },
                new Review
                {
                    Id = 9,
                    Title = "Venasaur",
                    Text = "Venasaur, Venasaur, Venasaur",
                    Rating = 1,
                    PokemonId = 3,
                    ReviewerId = 3
                }
            };

            db.Entity<Review>().HasData(reviews);
        }
    }
}