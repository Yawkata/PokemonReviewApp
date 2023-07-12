using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        public readonly DataContext context;

        public OwnerRepository(DataContext context) 
        {
            this.context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public List<Owner> GetOwners()
        {
            return context.Owners.ToList();
        }

        public List<Owner> GetOwnersOfAPokemon(int pokeId)
        {
            return context.PokemonOwners.Where(p => p.PokemonId == pokeId).Select(o => o.Owner).ToList();
        }

        public List<Pokemon> GetPokemonsByOwner(int ownerId)
        {
            return context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return context.Owners.Any(o => o.Id == ownerId);
        }

        public bool UpdateOwner(Owner owner)
        {
            context.Update(owner);
            return Save();
        }

        public bool Save()
        {
            return context.SaveChanges() > 0 ? true : false;
        }
    }
}
