using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class MovieControllerTests : FreeWheelMoviesAPITestBase
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Should_ReturnOKStatus_WhenMovieIsSaved(bool isSaved)
        {
            MockMovieService.Setup(s => s.SaveMovieAsync(It.IsAny<Movie>())).Returns(Task.FromResult(isSaved));

            var asyncResult = GetMoviesController().Post(new Movie());

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
    }
}