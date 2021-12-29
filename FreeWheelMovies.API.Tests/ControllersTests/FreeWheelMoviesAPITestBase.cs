using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class FreeWheelMoviesAPITestBase
    {

        protected Mock<IMovieService> MockMovieService;
        protected Mock<IMovieRatingService> MockMovieRatingService;
        protected Mock<IConfigurationSection> MockSection;
        protected Mock<IConfiguration> MockConfiguration;

        [SetUp]
        protected void Setup()
        {
            MockMovieService = new Mock<IMovieService>();
            MockMovieRatingService = new Mock<IMovieRatingService>();

            MockSection = new Mock<IConfigurationSection>();
            MockConfiguration = new Mock<IConfiguration>();
        }

        protected MoviesController GetMoviesController()
        {
            return new MoviesController(MockMovieService.Object);
        }
        protected MovieRatingController GetMovieRatingController()
        {
            return new MovieRatingController(MockMovieRatingService.Object);
        }
        protected MovieSearchController GetMovieSearchController()
        {
            return new MovieSearchController(MockMovieService.Object);
        }
        protected TopMovieController GetTopMoviesController()
        {
            return new TopMovieController(MockMovieRatingService.Object, GetConfiguration().Object);
        }
        protected Mock<IConfiguration> GetConfiguration()
        {
            MockSection.SetupGet(s => s[It.Is<string>(x => x == "NumberOfTopMovies")]).Returns("5");
            //MockConfiguration.SetupGet(s => s[It.Is<string>(x => x == "NumberOfTopMovies")]).Returns("5");
            //MockConfiguration.Setup(s => s[It.Is<string>(x => x == "NumberOfTopMovies")]).Returns("5");

            MockConfiguration.Setup(c => c.GetSection(It.IsAny<string>())).Returns(MockSection.Object);
            return MockConfiguration;
        }
    }
}