using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        PokemonResponse GetPokemons();

        PokemonResponse GetPokemon(int id);

        decimal GetPokemonRating(int id);

        PokemonResponse CreatePokemon(int ownerId, int categoryId, PokemonRequest pokemon);

        PokemonResponse UpdatePokemon(int pokeId, PokemonRequest pokemon);

        PokemonResponse DeletePokemon(int pokeId);

        bool PokemonExists(int id);

        bool PokemonExists(string name);

        bool Save();
    }
}
