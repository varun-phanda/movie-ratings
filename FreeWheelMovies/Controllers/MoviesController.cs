using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeWheelMovies.Controllers
{
    /// <summary>
    /// Movies Controller
    /// </summary>
    [Route("api/movie")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieSvc"></param>
        public MoviesController(IMovieService movieSvc)
        {
            this.movieService = movieSvc;
        }

        /// <summary>
        /// Get list of all movies
        /// </summary>
        /// <returns>Movies List as Json</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        public ActionResult<List<Movie>> Get()
        {
            var movies = movieService.GetAllMovies();
            return Json(movies);
        }

        /// <summary>
        /// Get a movie by ID
        /// </summary>
        /// <param name="id">ID of Movie</param>
        /// <returns>Movie Json</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public ActionResult<Movie> Get(int id)
        {
            var movie = movieService.GetMovie(id);
            return Json(movie);
        }

        /// <summary>
        /// Save a movie
        /// </summary>
        /// <param name="movie">Movie detail</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            try
            {
                if (await movieService.SaveMovieAsync(movie))
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Failed to Save the Movie");
                }
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
