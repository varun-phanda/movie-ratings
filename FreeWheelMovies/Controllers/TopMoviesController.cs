using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeWheelMovies.Controllers
{
    /// <summary>
    /// Movie Rating Controller
    /// </summary>
    [Route("api/movie/top")]
    [ApiController]
    public class TopMovieController : Controller
    {
        private readonly IMovieRatingService movieRatingService;
        private readonly IConfiguration config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieRatingSvc"></param>
        /// <param name="configuration"></param>
        public TopMovieController(IMovieRatingService movieRatingSvc, IConfiguration configuration)
        {
            this.config = configuration;
            this.movieRatingService = movieRatingSvc;
        }

        /// <summary>
        /// Get Top rated movies
        /// </summary>
        /// <returns>Json with the list of movies</returns>
        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            var numberOfTopMovies = config.GetValue<int>("NumberOfTopMovies",1);
            return await movieRatingService.GetTopRatedMoviesAsync(numberOfTopMovies);
        }

        /// <summary>
        /// Get Top rated movies for a user
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>Json with the list of movies</returns>
        [HttpGet("{userID}")]
        public async Task<ActionResult<List<Movie>>> Get(int userID)
        {
            var numberOfTopMovies = config.GetValue<int>("NumberOfTopMovies", 1);
            return await movieRatingService.GetTopRatedMovieByUserAsync(userID, numberOfTopMovies);
        }
    }
}
