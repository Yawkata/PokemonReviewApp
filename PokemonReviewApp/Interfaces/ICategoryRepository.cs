using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Dto.ResponseDTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        CategoryResponse GetCategories();

        CategoryResponse GetCategory(int id);

        List<Pokemon> GetPokemonByCategory(int id);

        bool CategoryExists(int id);

        bool CategoryExists(string categoryName);

        CategoryResponse CreateCategory(CategoryRequest category);

        CategoryResponse UpdateCategory(int categoryId, CategoryRequest category);

        CategoryResponse DeleteCategory(int categoryId);

        bool Save();

        
    }
}
