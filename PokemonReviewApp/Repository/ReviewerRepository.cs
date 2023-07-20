using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Dto.ResponseDTOs;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Helper;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using AutoMapper;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReviewerResponse GetReviewers()
        {
            ReviewerResponse response = new();

            var reviewers = _context.Reviewers.OrderBy(c => c.Id).ToList();

            response.Reviewers = _mapper.Map<List<ReviewerDto>>(reviewers);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewerResponse GetReviewer(int reviewerId)
        {
            ReviewerResponse response = new();

            if (!ReviewerExists(reviewerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Reviewer");
                return response;
            }

            var reviewer = _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
            response.Reviewers.Add(_mapper.Map<ReviewerDto>(reviewer));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewResponse GetReviewsByReviewer(int reviewerId)
        {
            ReviewResponse response = new();

            if (!ReviewerExists(reviewerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Reviewer");
                return response;
            }

            var reviews = _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();

            response.Reviews = _mapper.Map<List<ReviewDto>>(reviews);
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewerResponse CreateReviewer(ReviewerRequest reviewerCreate)
        {
            ReviewerResponse response = new();

            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            _context.Add(reviewerMap);
            Save();

            response.Reviewers.Add(_mapper.Map<ReviewerDto>(reviewerMap));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewerResponse UpdateReviewer(int reviewerId, ReviewerRequest reviewerUpdate)
        {
            ReviewerResponse response = new();

            if (!ReviewerExists(reviewerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Reviewer");
                return response;
            }

            var reviewer = _mapper.Map<Reviewer>(reviewerUpdate);
            reviewer.Id = reviewerId;

            _context.Update(reviewer);
            Save();
            ;
            response.Reviewers.Add(_mapper.Map<ReviewerDto>(reviewer));
            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public ReviewerResponse DeleteReviewer(int reviewerId)
        {
            ReviewerResponse response = new();

            if (!ReviewerExists(reviewerId))
            {
                response.ServerMessage = String.Format(GlobalConstants.Notfound, "Reviewer");
                return response;
            }

            var reviewerToDelte = _context.Reviewers.FirstOrDefault(c => c.Id == reviewerId);

            _context.Remove(reviewerToDelte);
            Save();

            response.ServerMessage = GlobalConstants.Success;

            return response;
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
