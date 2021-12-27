using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeWheelMovies.Controllers
{
    /// <summary>
    /// Movie Rating Controller
    /// </summary>
    [Route("api/movie/rating")]
    [ApiController]
    public class MovieRatingController : Controller
    {
        private readonly IMovieRatingService movieRatingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieRatingSvc"></param>
        public MovieRatingController(IMovieRatingService movieRatingSvc)
        {
            this.movieRatingService = movieRatingSvc;
        }
        
        /// <summary>
        /// Get Average rating for a movie
        /// </summary>
        /// <param name="movieID">movie Id</param>
        /// <returns>Returns the rating rounded to closest 0.5</returns>
        [HttpGet("{movieID}")]
        public ActionResult<double> Get(int movieID)
        {
            var rating = movieRatingService.GetMovieRating(movieID);
            return Json(rating);
        }

        /// <summary>
        /// Save the Review for a Movie
        /// </summary>
        /// <param name="movieRating">Movie's Rating</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieRating movieRating)
        {
            try
            {
                if (await movieRatingService.SaveMovieReviewAsync(movieRating))
                {
                    return Ok("Movie Rating saved successfully");
                }
                else
                {
                    return BadRequest("Failed to Save the Movie Review");
                }
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
