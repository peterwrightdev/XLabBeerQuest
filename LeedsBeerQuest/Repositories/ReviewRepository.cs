using LeedsBeerQuest.Contracts;
using System.Collections.Generic;
using LeedsBeerQuest.CustomExceptions;
using LeedsBeerQuest.Mapping;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;

namespace LeedsBeerQuest.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _filePath;
        public ReviewRepository(string filePath)
        {
            _filePath = filePath;
        }
        public Review GetReview(string name)
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return DTOMapper.ConvertFromSource(csv.GetRecords<ReviewSourceDTO>().Where(r => r.name == name).First());
                }
            }
            catch
            {
                throw new ExpectedDataNotFoundException($"Could not find review for {name}");
            }
        }

        public IEnumerable<Review> GetAllReviews()
        {
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var enumerable = csv.GetRecords<ReviewSourceDTO>().Select(s => DTOMapper.ConvertFromSource(s));
                var enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public IEnumerable<Review> GetFilteredReviewSourceDTOs(ReviewFilters filters)
        {
            return GetAllReviews().Where(s => 
                s.AmenitiesRating >= (filters.minAmenities ?? s.AmenitiesRating)
                && s.AtmosphereRating >= (filters.minAtmosphere ?? s.AtmosphereRating)
                && s.BeersRating >= (filters.minBeer ?? s.BeersRating)
                && s.ValueRating >= (filters.minValue ?? s.ValueRating)
                && s.DateTime >= (filters.dateFrom ?? s.DateTime)
                && s.VenueCategory == (filters.venueCategory ?? s.VenueCategory)
                && s.Tags.Intersect(filters.tags ?? new List<string>()).Count() == (filters.tags?.Count() ?? 0));
        }

        public void CreateReview(Review review)
        {
            using (var steam = File.Open(_filePath, FileMode.Append))
            using (var writer = new StreamWriter(steam))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header.
                HasHeaderRecord = false,
            }))
            {
                csv.WriteRecord(DTOMapper.ConvertToSource(review));
            }
        }

        public void DeleteReview(string name)
        {
            // not a fan of this but apparently no choice with this library
            // https://github.com/JoshClose/CsvHelper/issues/1397
            var reviews = GetAllReviews();
            var tempNewFileName = Guid.NewGuid().ToString() + "_" + _filePath;
            using (var steam = File.Open(tempNewFileName, FileMode.OpenOrCreate))
            using (var writer = new StreamWriter(steam))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteHeader<ReviewSourceDTO>();
                foreach (var review in reviews)
                {
                    if (review.Name != name)
                    {
                        csv.NextRecord();
                        csv.WriteRecord(DTOMapper.ConvertToSource(review));
                    }
                }
            }

            File.Delete(_filePath);
            File.Move(tempNewFileName, _filePath);
        }
    }

    public interface IReviewRepository
    {
        Review GetReview(string name);
        IEnumerable<Review> GetAllReviews();
        IEnumerable<Review> GetFilteredReviewSourceDTOs(ReviewFilters filters);
        void CreateReview(Review review);
        void DeleteReview(string name);
    }
}
