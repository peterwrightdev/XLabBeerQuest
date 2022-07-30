using LeedsBeerQuest.Controllers;
using LeedsBeerQuest.Repositories;
using System.Linq.Expressions;

namespace LeedsBeerQuest.Mapping
{
    // Could use something like AutoMapper instead of this, but seemed excessive for this use case
    public static class DTOMapper
    {
        public static Review ConvertFromSource(ReviewSourceDTO source)
        {
            return new Review(source.name,
                source.category,
                new Uri(source.url),
                DateTime.Parse(source.date),
                source.excerpt,
                new Uri(source.thumbnail),
                source.lat,
                source.lng,
                source.address,
                // probably should fix phone numbers here
                source.phone,
                source.twitter,
                source.stars_beer,
                source.stars_atmosphere,
                source.stars_amenities,
                source.stars_value,
                source.tags.Split(',').ToList());
        }

        public static ReviewSourceDTO ConvertToSource(Review review)
        {
            return new ReviewSourceDTO(review.Name,
                review.VenueCategory,
                review.ReviewUri.AbsoluteUri,
                review.DateTime.ToString(),
                review.Excerpt,
                review.Thumbnail.AbsoluteUri,
                review.Latitude,
                review.Longitude,
                review.Address,
                review.PhoneNumber,
                review.TwitterHandle,
                review.BeersRating,
                review.AtmosphereRating,
                review.AmenitiesRating,
                review.ValueRating,
                review.Tags.Skip(1).Aggregate(review.Tags.FirstOrDefault() ?? string.Empty, (result, latest) => result + "," + latest, final => final));
        }

        public static Review ConvertFromClient(ReviewClientDTO client)
        {
            return new Review(client.Name,
                client.VenueCategory,
                client.ReviewUri,
                client.DateTime,
                client.Excerpt,
                client.Thumbnail,
                client.Latitude,
                client.Longitude,
                client.Address,
                client.PhoneNumber,
                client.TwitterHandle,
                client.BeersRating,
                client.AtmosphereRating,
                client.AmenitiesRating,
                client.ValueRating,
                client.Tags);
        }
        public static ReviewClientDTO ConvertToClient(Review review)
        {
            return new ReviewClientDTO(review.Name,
                review.VenueCategory,
                review.ReviewUri,
                review.DateTime,
                review.Excerpt,
                review.Thumbnail,
                review.Latitude,
                review.Longitude,
                review.Address,
                review.PhoneNumber,
                review.TwitterHandle,
                review.BeersRating,
                review.AtmosphereRating,
                review.AmenitiesRating,
                review.ValueRating,
                review.Tags);
        }
    }
}
