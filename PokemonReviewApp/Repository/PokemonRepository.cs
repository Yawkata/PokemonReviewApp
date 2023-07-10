using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        public readonly DataContext context;

        public PokemonRepository(DataContext context) 
        {     
            this.context = context;
        }

        public List<Pokemon> GetPokemons()
        {
            return context.Pokemon.OrderBy(p => p.Id).ToList();
        }

        public Pokemon GetPokemon(int id)
        {
            return context.Pokemon.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return context.Pokemon.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = context.Reviews.Where(p => p.Pokemon.Id == id);

            if (review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Average(r => r.Rating));
        }

        public bool PokemonExists(int id)
        {
            if (context.Pokemon.Where(p => p.Id == id).Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
