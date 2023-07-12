using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        List<Owner> GetOwners();

        Owner GetOwner(int id);

        bool OwnerExists(int id);

        List<Owner> GetOwnersOfAPokemon(int id);

        List<Pokemon> GetPokemonsByOwner(int id);

        bool CreateOwner(Owner owner);

        bool UpdateOwner(Owner owner);

        bool Save();

    }
}
 