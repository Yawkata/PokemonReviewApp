using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ReviewResponse GetReviews();

        ReviewResponse GetReview(int reviewId);

        ReviewResponse GetReviewsOfAPokemon(int pokeId);

        ReviewResponse CreateReview(int reviewwerId, int pokeId, ReviewRequest review);

        ReviewResponse UpdateReview(int reviewId, ReviewRequest review);

        ReviewResponse DeleteReview(int reviewId);

        bool ReviewExists(int reviewId);

        bool ReviewExists(string reviewTitle);

        bool DeleteReviews(List<Review> reviews);

        bool Save();
    }
}
