using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class TopMovieControllerTests : FreeWheelMoviesAPITestBase
    {
        [Test]
        public void Should_ReturnValid_Get()
        {
            var mockMoviesList = new List<Movie>()
                {
                    new Movie()
                    {
                        Title="Mock",
                        Description="Mock",
                        ReleaseDate = DateTime.Now,
                        IsActive = true,
                        Genre = "1"
                    }
                };
            MockMovieRatingService.Setup(s => s.GetTopRatedMoviesAsync(It.IsAny<int>())).Returns(Task.FromResult(new List<Movie>(mockMoviesList)));

            var asyncResult = GetTopMoviesController().Get();

            var result = asyncResult.Result;
            Assert.IsInstanceOf<JsonResult>(result.Result);
            Assert.AreEqual((((JsonResult)result.Result).Value as List<Movie>).Count, mockMoviesList.Count);
        }

        [Test]
        public void Should_ReturnNotFound_NoMovies_Get()
        {
            MockMovieRatingService.Setup(s => s.GetTopRatedMoviesAsync(It.IsAny<int>())).Returns(Task.FromResult(null as List<Movie>));

            var asyncResult = GetTopMoviesController().Get();

            var result = asyncResult.Result;
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnValid_GetByUserID(bool isUserValid)
        {
            var mockMoviesList = new List<Movie>()
                {
                    new Movie()
                    {
                        Title="Mock",
                        Description="Mock",
                        ReleaseDate = DateTime.Now,
                        IsActive = true,
                        Genre = "1"
                    }
                };
            if (isUserValid)
            {
                MockMovieRatingService.Setup(s => s.GetTopRatedMoviesByUserAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(mockMoviesList));
            }
            else
            {
                MockMovieRatingService.Setup(s => s.GetTopRatedMoviesByUserAsync(It.IsAny<int>(), It.IsAny<int>())).Throws(new ArgumentException("User not Found", "MovieRating"));
            }

            var asyncResult = GetTopMoviesController().Get(1);

            var result = asyncResult.Result;
            if (isUserValid)
            {
                Assert.IsInstanceOf<JsonResult>(result.Result);
                Assert.AreEqual((((JsonResult)result.Result).Value as List<Movie>).Count, mockMoviesList.Count);
            }
            else
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            }
        }
    }
}