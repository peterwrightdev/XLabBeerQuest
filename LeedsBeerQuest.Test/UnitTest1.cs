using System;
using System.Collections.Generic;
using LeedsBeerQuest.Mapping;
using LeedsBeerQuest.Validation;
using NUnit.Framework;

namespace LeedsBeerQuest.Test
{
    public class ReviewValidatorTests
    {

        [Test]
        [TestCase(-181, 0, 0, 0, 0, 0)]
        [TestCase(181, 0, 0, 0, 0, 0)]
        [TestCase(0, -91, 0, 0, 0, 0)]
        [TestCase(0, 91, 0, 0, 0, 0)]
        [TestCase(0, 0, -1, 0, 0, 0)]
        [TestCase(0, 0, 6, 0, 0, 0)]
        [TestCase(0, 0, 0, -1, 0, 0)]
        [TestCase(0, 0, 6, 0, 0, 0)]
        [TestCase(0, 0, 0, -1, 0, 0)]
        [TestCase(0, 0, 0, 6, 0, 0)]
        [TestCase(0, 0, 0, 0, -1, 0)]
        [TestCase(0, 0, 0, 0, 6, 0)]
        [TestCase(0, 0, 0, 0, 0, -1)]
        [TestCase(0, 0, 0, 0, 0, 6)]
        public void ReviewValidatorRejectsFailures(
            decimal longitude,
            decimal latitude,
            decimal beersRating,
            decimal atmosphereRating,
            decimal amenitiesRating,
            decimal valueRating)
        {
            // arrange
            var review = new Review(
                "name",
                "category",
                new Uri("https://localhost:443/"),
                new DateTime(2022, 03, 12),
                string.Empty,
                new Uri("https://localhost:443/"),
                latitude,
                longitude,
                string.Empty,
                string.Empty,
                string.Empty,
                beersRating,
                atmosphereRating,
                amenitiesRating,
                valueRating,
                new List<string>());
            var validator = new ReviewValidator();

            // act
            var pass = validator.Validate(review);

            // assert
            Assert.IsFalse(pass);
        }
    }
}