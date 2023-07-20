using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ReviewerResponse GetReviewers();

        ReviewerResponse GetReviewer(int reviewerId);

        ReviewResponse GetReviewsByReviewer(int reviewerId);

        ReviewerResponse CreateReviewer(ReviewerRequest reviewer);

        ReviewerResponse UpdateReviewer(int reviewerID, ReviewerRequest reviewer);

        ReviewerResponse DeleteReviewer(int reviewerId);

        bool ReviewerExists(int reviewerId);

        bool Save();
    }
}
