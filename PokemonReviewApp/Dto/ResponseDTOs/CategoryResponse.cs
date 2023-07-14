using PokemonReviewApp.Models;

namespace PokemonReviewApp.Dto.ResponseDTOs
{
    public class CategoryResponse
    {
        public CategoryResponse()
        {
            this.Categories = new List<CategoryDto>();
        }

        public List<CategoryDto> Categories { get; set; }
        public string ServerMessage { get; set; }
    }
}
