namespace LeedsBeerQuest.Contracts
{
    public record ReviewFilters(
        string? venueCategory,
        DateTime? dateFrom,
        decimal? minBeer,
        decimal? minAtmosphere,
        decimal? minAmenities,
        decimal? minValue,
        List<string>? tags);
}