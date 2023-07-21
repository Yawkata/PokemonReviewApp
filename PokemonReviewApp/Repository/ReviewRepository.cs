using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public ReviewResponse GetReviews()
        {
            ReviewResponse response = new();

            var reviews = _context.Reviews.OrderBy(c => c.Id).ToList();

            response.Reviews = _mapper.Map<List<ReviewDto>>(reviews);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewResponse GetReview(int reviewId)
        {
            ReviewResponse response = new();

            if (!ReviewExists(reviewId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Review");
                return response;
            }

            var review = _context.Reviews.FirstOrDefault(c => c.Id == reviewId);
            response.Reviews.Add(_mapper.Map<ReviewDto>(review));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewResponse GetReviewsOfAPokemon(int pokeId)
        {
            ReviewResponse response = new();

            if (!_context.Pokemon.Any(p => p.Id == pokeId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Pokemon");
                return response;
            }

            var reviews = _context.Pokemon.Where(p => p.Id == pokeId).Select(p => p.Reviews).FirstOrDefault();

            response.Reviews = _mapper.Map<List<ReviewDto>>(reviews);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewResponse CreateReview(int reviewerId, int pokeId, ReviewRequest reviewCreate)
        {
            ReviewResponse response = new();

            if (ReviewExists(reviewCreate.Title))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Review");
                return response;
            }

            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Reviewer = _context.Reviewers.Where(p => p.Id == reviewerId).FirstOrDefault();
            reviewMap.ReviewerId = reviewerId;

            reviewMap.Pokemon = _context.Pokemon.Where(p => p.Id == pokeId).FirstOrDefault();
            reviewMap.PokemonId = pokeId;
            

            _context.Add(reviewMap);
            Save();

            var review = _context.Reviews.FirstOrDefault(x => x.Title == reviewCreate.Title);
            response.Reviews.Add(_mapper.Map<ReviewDto>(review));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewResponse UpdateReview(int reviewId, ReviewRequest reviewUpdate)
        {
            ReviewResponse response = new();

            if (!ReviewExists(reviewId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Review");
                return response;
            }

            if (ReviewExists(reviewUpdate.Title))
            {
                response.ServerMessage = String.Format(GlobalConstants.AlreadyExists, "Review");
                return response;
            }
            var review = _context.Reviews.FirstOrDefault(r => r.Id == reviewId);
            review.Text = reviewUpdate.Text;
            review.Title = reviewUpdate.Title;
            review.Rating = reviewUpdate.Rating;
            //var review = _mapper.Map<Review>(reviewUpdate);
            //review.Id = reviewId;



            //_context.Update(review);
            Save();

            response.Reviews.Add(_mapper.Map<ReviewDto>(review));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewResponse DeleteReview(int reviewId)
        {
            ReviewResponse response = new();

            if (!ReviewExists(reviewId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Review");
                return response;
            }

            var reviewToDelete = _context.Reviews.FirstOrDefault(c => c.Id == reviewId);

            _context.Remove(reviewToDelete);
            Save();

            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool ReviewExists(string title)
        {
            return _context.Reviews.Any(r => r.Title.ToUpper() == title.ToUpper());
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
