using FreeWheelMovies.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FreeWheelMovies.Data
{
    public class FreeWheelMovieDbContext : DbContext
    {
        public FreeWheelMovieDbContext(DbContextOptions<FreeWheelMovieDbContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasKey(mv => new { mv.ID });
            modelBuilder.Entity<Movie>().HasQueryFilter(mv => mv.IsActive);
        }
    }
}
