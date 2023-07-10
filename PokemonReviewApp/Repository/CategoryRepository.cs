using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly DataContext context;
        public CategoryRepository(DataContext context) 
        {
            this.context = context;
        }
        public bool CategoryExists(int categoryId)
        {
            return context.Categories.Where(c => c.Id == categoryId).Any();
        }

        public List<Category> GetCategories()
        {
            return context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return context.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public List<Pokemon> GetPokemonByCategory(int categoryId)
        {
            var categories = context.PokemonCategories.Where(c => c.CategoryId == categoryId);
            List<Pokemon> pokemons = categories.Select(c => c.Pokemon).ToList();

            return pokemons;
        }
    }
}
