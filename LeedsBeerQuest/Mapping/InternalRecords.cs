namespace LeedsBeerQuest.Mapping
{
    public record Review(
        string Name,
        string VenueCategory,
        Uri ReviewUri,
        DateTime DateTime,
        string Excerpt,
        Uri Thumbnail,
        decimal Latitude,
        decimal Longitude,
        string Address,
        string? PhoneNumber,
        string? TwitterHandle,
        decimal BeersRating,
        decimal AtmosphereRating,
        decimal AmenitiesRating,
        decimal ValueRating,
        List<string> Tags);
}
