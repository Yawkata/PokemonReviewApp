using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();

        Category GetCategory(int id);

        List<Pokemon> GetPokemonByCategory(int id);

        bool CategoryExists(int id);
    }
}
