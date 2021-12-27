using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeWheelMovies.Controllers
{
    /// <summary>
    /// Search Movie controller
    /// </summary>
    [Route("api/movie/search")]
    [ApiController]
    public class MovieSearchController : Controller
    {
        private readonly IMovieService movieService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movieSvc"></param>
        public MovieSearchController(IMovieService movieSvc)
        {
            this.movieService = movieSvc;
        }        

        /// <summary>
        /// Search a movie by properties of Movie
        /// </summary>
        /// <param name="Title">Title of the movie</param>
        /// <param name="YearOfRelease">Year of movie's release</param>
        /// <param name="Genre">Movie's Genre</param>
        /// <returns>List of Movies as Json</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        public async Task<IActionResult> Get(string Title, string YearOfRelease, string Genre)
        {
            try
            {
                var movies = await movieService.SearchMoviesAsync(Title, YearOfRelease, Genre);
                if (movies == null || !movies.Any())
                {
                    return NotFound("No Movie found based on the criteria");
                }
                return Json(movies);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
