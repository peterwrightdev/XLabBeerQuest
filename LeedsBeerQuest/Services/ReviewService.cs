using LeedsBeerQuest.Contracts;
using LeedsBeerQuest.CustomExceptions;
using LeedsBeerQuest.Mapping;
using LeedsBeerQuest.Repositories;
using LeedsBeerQuest.Validation;

namespace LeedsBeerQuest.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IValidateReviews _validator;
        public ReviewService(IReviewRepository repository, IValidateReviews validator)
        {
            _reviewRepository = repository;
            _validator = validator;
        }

        public void CreateReview(Review review)
        {
            if (_validator.Validate(review))
            {
                _reviewRepository.CreateReview(review);
                return;
            }

            throw new InvalidReviewException("Review failed validation");
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _reviewRepository.GetAllReviews();
        }

        public IEnumerable<Review> GetFilteredReviewSourceDTOs(ReviewFilters filters)
        {
            return _reviewRepository.GetFilteredReviewSourceDTOs(filters);
        }

        public Review GetReview(string name)
        {
            return _reviewRepository.GetReview(name);
        }
        public void DeleteReview(string name)
        {
            _reviewRepository.DeleteReview(name);
        }
    }

    public interface IReviewService
    {
        Review GetReview(string name);
        IEnumerable<Review> GetAllReviews();
        IEnumerable<Review> GetFilteredReviewSourceDTOs(ReviewFilters filters);
        void CreateReview(Review review);
        void DeleteReview(string name);
    }
}
