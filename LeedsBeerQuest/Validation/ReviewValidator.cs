using LeedsBeerQuest.Controllers;
using LeedsBeerQuest.Mapping;

namespace LeedsBeerQuest.Validation
{
    public class ReviewValidator : IValidateReviews
    {
        public bool Validate(Review review)
        {
            if (review.AmenitiesRating >= 0
                && review.AmenitiesRating <= 5
                && review.AtmosphereRating >= 0
                && review.AtmosphereRating <= 5
                && review.BeersRating >= 0
                && review.BeersRating <= 5
                && review.Latitude >= -90
                && review.Latitude <= 90
                && review.Longitude >= -180
                && review.Longitude <= 180
                && review.ValueRating >= 0
                && review.ValueRating <= 5)
            {
                return true;
            }
            return false;
        }
    }

    public interface IValidateReviews
    {
        bool Validate(Review review);
    }
}
