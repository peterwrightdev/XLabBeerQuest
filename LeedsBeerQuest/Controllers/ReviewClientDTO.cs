namespace LeedsBeerQuest.Controllers
{
    public record ReviewClientDTO(
        string Name,
        string VenueCategory,
        Uri ReviewUri,
        DateTime DateTime,
        string Excerpt,
        Uri Thumbnail,
        decimal Latitude,
        decimal Longitude,
        string Address,
        // Looks optional, possibly proceeding zeroes dropped by xls file
        // might be fixable by taking .csv, try both
        string? PhoneNumber,
        string? TwitterHandle,
        decimal BeersRating,
        decimal AtmosphereRating,
        decimal AmenitiesRating,
        decimal ValueRating,
        List<string> Tags);
}
