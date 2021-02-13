using System.Collections.Generic;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterface;

namespace MovieShop.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> GetTopRevenueMovies()
        {
            var movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000}
            };
            return movies;
        }

        public IEnumerable<Movie> GetTopRatedMovies()
        {
            var movies = new List<Movie>
            {
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
            };
            return movies;
        }
    }
}