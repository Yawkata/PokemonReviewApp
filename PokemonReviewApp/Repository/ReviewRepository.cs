using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        public readonly DataContext context;

        public ReviewRepository(DataContext context) 
        {
            this.context = context;
        }
        public Review GetReview(int reviewId)
        {
            return context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public List<Review> GetReviews()
        {
            return context.Reviews.ToList();
        }

        public List<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return context.Pokemon.Where(p => p.Id == pokeId).Select(p => p.Reviews).FirstOrDefault();
        }

        public bool ReviewExists(int reviewId)
        {
            return context.Reviews.Any(r => r.Id == reviewId);
        }
    }
}
