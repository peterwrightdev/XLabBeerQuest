namespace LeedsBeerQuest.Repositories
{
    // only one review per bar name?
    // should Bar, Pub and Closed be separate types?
    // 
    // check spelling of lat and long
    public record ReviewSourceDTO(
        string name,
        string category,
        string url,
        string date,
        string excerpt,
        string thumbnail,
        decimal lat,
        decimal lng,
        string address,
        // Looks optional, possibly proceeding zeroes dropped by xls file
        // might be fixable by taking .csv, try both
        string? phone,
        string? twitter,
        decimal stars_beer,
        decimal stars_atmosphere,
        decimal stars_amenities,
        decimal stars_value,
        string tags);

}
