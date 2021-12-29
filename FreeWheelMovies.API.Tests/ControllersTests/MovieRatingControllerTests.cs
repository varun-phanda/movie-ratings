using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class MovieRatingControllerTests : FreeWheelMoviesAPITestBase
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnOKStatus_WhenMovieRatingIsSaved(bool isSaved)
        {
            MockMovieRatingService.Setup(s => s.SaveMovieReviewAsync(It.IsAny<MovieRating>())).Returns(Task.FromResult(isSaved));

            var asyncResult = GetMovieRatingController().Post(new MovieRating());

            var result = asyncResult.Result;

            if (isSaved)
            {
                Assert.IsInstanceOf<OkObjectResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

        [Test]
        public void Should_ReturnValid_Get()
        {
            MockMovieRatingService.Setup(s => s.GetMovieRating(It.IsAny<int>())).Returns(Task.FromResult(2.65));

            var asyncResult = GetMovieRatingController().Get(1);

            var result = asyncResult.Result;
            Assert.IsInstanceOf<JsonResult>(result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnNotFound_GetWithMovieNullOrEmpty(bool isNullMovie)
        {
            if (isNullMovie)
            {
                MockMovieRatingService.Setup(s => s.GetMovieRating(It.IsAny<int>())).Throws(new ArgumentException("Movie not Found", "MovieRating"));
            }
            else
            {
                MockMovieRatingService.Setup(s => s.GetMovieRating(It.IsAny<int>())).Returns(Task.FromResult(2.65));
            }
            var asyncResult = GetMovieRatingController().Get(1);

            var result = asyncResult.Result;
            if (isNullMovie)
            {
                Assert.IsInstanceOf<NotFoundObjectResult>(result);
            }
            else
            {
                Assert.IsInstanceOf<JsonResult>(result);
            }
        }

        public void Should_ReturnNotFound_PostWithInvalidMovie()
        {
            MockMovieRatingService.Setup(s => s.SaveMovieReviewAsync(It.IsAny<MovieRating>())).Throws(new ArgumentException("Movie not Found", "MovieRating"));
            
            var asyncResult = GetMovieRatingController().Post(new MovieRating());

            var result = asyncResult.Result;
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        public void Should_ReturnNotFound_PostWithInvalidUser()
        {
            MockMovieRatingService.Setup(s => s.SaveMovieReviewAsync(It.IsAny<MovieRating>())).Throws(new ArgumentException("User not Found", "MovieRating"));

            var asyncResult = GetMovieRatingController().Post(new MovieRating());

            var result = asyncResult.Result;
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        public void Should_ReturnBadRequest_PostWithInvalidRating()
        {
            MockMovieRatingService.Setup(s => s.SaveMovieReviewAsync(It.IsAny<MovieRating>())).Throws(new ArgumentOutOfRangeException("Invalid Rating", "MovieRating"));

            var asyncResult = GetMovieRatingController().Post(new MovieRating());

            var result = asyncResult.Result;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}