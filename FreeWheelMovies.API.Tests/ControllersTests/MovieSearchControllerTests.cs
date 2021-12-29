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
    public class MovieSearchControllerTests : FreeWheelMoviesAPITestBase
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
            MockMovieService.Setup(s => s.SearchMoviesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new List<Movie>(mockMoviesList)));

            var asyncResult = GetMovieSearchController().Get("title","1988","2");

            var result = asyncResult.Result;
            Assert.IsInstanceOf<JsonResult>(result);
            Assert.AreEqual((((JsonResult)result).Value as List<Movie>).Count, mockMoviesList.Count);
        }

        [Test]
        public void Should_ReturnBadRequest_GetWithEmptySearchCriteria()
        {
            MockMovieService.Setup(s => s.SearchMoviesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException("Invalid Criteria - No criteria provided"));

            var asyncResult = GetMovieSearchController().Get("", "", "");

            var result = asyncResult.Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Should_ReturnBadRequest_GetWithInvalidDate()
        {
            MockMovieService.Setup(s => s.SearchMoviesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException("Invalid Year of release"));

            var asyncResult = GetMovieSearchController().Get("", "InvalidYear", "");

            var result = asyncResult.Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}