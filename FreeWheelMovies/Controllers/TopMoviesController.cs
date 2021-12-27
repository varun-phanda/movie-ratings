using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                var numberOfTopMovies = config.GetValue<int>("NumberOfTopMovies", 1);
                var topMovies = await movieRatingService.GetTopRatedMoviesAsync(numberOfTopMovies);
                if (topMovies.Count() <= 0)
                {
                    return NotFound("No movie found");
                }
                return Json(topMovies);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Top rated movies for a user
        /// </summary>
        /// <param name="userID">User Id</param>
        /// <returns>Json with the list of movies</returns>
        [HttpGet("{userID}")]
        public async Task<ActionResult<List<Movie>>> Get(int userID)
        {
            try
            {
                var numberOfTopMovies = config.GetValue<int>("NumberOfTopMovies", 1);
                var topMovies = await movieRatingService.GetTopRatedMoviesByUserAsync(userID, numberOfTopMovies);
                if (topMovies.Count() <= 0)
                {
                    return NotFound("No movie found");
                }
                return Json(topMovies);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
