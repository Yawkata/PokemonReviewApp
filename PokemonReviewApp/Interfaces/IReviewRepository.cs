using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetReviews();

        Review GetReview(int reviewId);

        List<Review> GetReviewsOfAPokemon(int pokeId);

        bool ReviewExists(int reviewId);
    }
}
