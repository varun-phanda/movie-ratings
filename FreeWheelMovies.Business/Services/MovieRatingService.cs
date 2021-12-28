using FreeWheelMovies.Data;
using FreeWheelMovies.Shared.Entities;
using FreeWheelMovies.Business.Interfaces;
using System.Threading.Tasks;
using FreeWheelMovies.Data.DataManager;
using FreeWheelMovies.Data.DataManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeWheelMovies.Business
{
    public class MovieRatingService : IMovieRatingService
    {

        private readonly IMovieRatingDataManager movieRatingDataManager;
        private readonly IMovieDataManager movieDataManager;
        private readonly IUserDataManager userDataManager;

        public MovieRatingService(IMovieRatingDataManager movieRatingDM, IMovieDataManager movieDM, IUserDataManager userDM)
        {
            this.movieRatingDataManager = movieRatingDM;
            this.movieDataManager = movieDM;
            this.userDataManager = userDM;
        }
                
        /// <summary>
        /// Get Average rating for a movie
        /// </summary>
        /// <param name="movieID">movie Id</param>
        /// <returns>Returns the rating rounded to closest 0.5</returns>
        public async Task<double> GetMovieRating(int movieID)
        {
            return movieRatingDataManager.GetMovieAverageRating(movieID);

        }

        /// <summary>
        /// Save the MovieReview Asynchronously
        /// </summary>
        /// <param name="rating">MovieReview object</param>
        /// <returns>Boolean confirming whether saved or not</returns>
        public async Task<bool> SaveMovieReviewAsync(MovieRating rating)
        {
            //Validate
            if (!IsValidMovieRating(rating))
            {
                return false;
            }
            //Save Movie Rating
            if (!await movieRatingDataManager.SaveMovieRatingAsync(rating))
            {
                return false;
            }
            //Update Average Rating
            var averageRating = movieRatingDataManager.GetMovieAverageRating(rating.MovieID);
            return await movieDataManager.UpdateMovieAverageRatingAsync(rating.MovieID, averageRating);
        }

        /// <summary>
        /// To Validate the movie rating properties
        /// </summary>
        /// <param name="rating">MovieRating object</param>
        /// <returns>Boolean</returns>
        private bool IsValidMovieRating(MovieRating rating)
        {
            if (rating.MovieID == 0 || movieDataManager.GetMovie(rating.MovieID) == null)
            {
                throw new ArgumentException("Movie not Found", "MovieRating");
            }
            if (rating.UserID == 0 || userDataManager.GetUser(rating.UserID) == null)
            {
                throw new ArgumentException("User not Found", "MovieRating");
            }
            if (rating.Rating == null || !Enum.IsDefined(typeof(RatingStars), rating.Rating))
            { 
                throw new ArgumentOutOfRangeException("Invalid Rating", "MovieRating");
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noOfRecords"></param>
        /// <returns></returns>
        public async Task<List<Movie>> GetTopRatedMoviesAsync(int noOfRecords)
        {
            //var movieRatings = await movieRatingDataManager.GetTopRatedMoviesAsync(noOfRecords);
            //var movies = await movieDataManager.GetAllMovies();
            //List<Movie> topRatedMovies = null;
            //if (movieRatings.Count() > 0 && movies.Count() > 0)
            //{
            //    topRatedMovies = movies.Join(movieRatings, mv => mv.ID, mvr => mvr.MovieID, (mv, mvr) => new Movie
            //    {
            //        AverageRating = (int)mvr.Rating,
            //        Title = mv.Title,
            //        Description = mv.Description,
            //        Genre = mv.Genre,
            //        ReleaseDate = mv.ReleaseDate,
            //        ID = mv.ID
            //    }).OrderByDescending(mv=>mv.AverageRating).ThenBy(mv=>mv.Title).ToList();
            //}
            //return topRatedMovies;

            return await movieDataManager.GetTopMoviesAsync(noOfRecords);
        }

        /// <summary>
        /// Get User's TopRated Movies
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="numberOfTopMovies"></param>
        /// <returns>List of Movies</returns>
        public async Task<List<Movie>> GetTopRatedMoviesByUserAsync(int userID, int numberOfTopMovies)
        {
            //Validate UserID
            if(userDataManager.GetUser(userID)==null)
            {
                throw new ArgumentException("User Not found");
            }
            var movieRatings = await movieRatingDataManager.GetTopMovieRatingsByUserAsync(userID);
            var movies = await movieDataManager.GetAllMovies();
            List<Movie> topRatedMovies  = null;
            if (movieRatings.Count() > 0 && movies.Count() > 0)
            {
                topRatedMovies = movies.Join(movieRatings, mv => mv.ID, mvr => mvr.MovieID, (mv, mvr) => new Movie
                {
                    AverageRating = (int)mvr.Rating,
                    Title = mv.Title,
                    Description = mv.Description,
                    Genre = mv.Genre,
                    ReleaseDate = mv.ReleaseDate,
                    ID = mv.ID
                }).OrderByDescending(mv => mv.AverageRating).ThenBy(mv => mv.Title).Take(numberOfTopMovies).ToList();
            }
            return topRatedMovies;
        }
    }
}
