using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterface;
using MovieShop.Core.ServiceInterface;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly ICurrentUser _currentUser;
        private readonly IAsyncRepository<Purchase> _purchaseRepository;
        private readonly IMovieRepository _repository;
        private readonly IReviewRepository _reviewRepository;

        public MovieService(IMovieRepository repository, ICurrentUser currentUser,
            IAsyncRepository<Purchase> purchaseRepository, IReviewRepository reviewRepository)
        {
            _repository = repository;
            _currentUser = currentUser;
            _purchaseRepository = purchaseRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<MovieDetailsResponse> GetMovieById(int id)
        {
            var movieDetail = new MovieDetailsResponse();
            var movie = await _repository.GetByIdAsync(id);
            movieDetail.Id = movie.Id;
            movieDetail.Title = movie.Title;
            movieDetail.Overview = movie.Overview;
            movieDetail.Tagline = movie.Tagline;
            movieDetail.Budget = movie.Budget;
            movieDetail.Revenue = movie.Revenue;
            movieDetail.ImdbUrl = movie.ImdbUrl;
            movieDetail.TmdbUrl = movie.TmdbUrl;
            movieDetail.PosterUrl = movie.PosterUrl;
            movieDetail.BackdropUrl = movie.BackdropUrl;
            movieDetail.OriginalLanguage = movie.OriginalLanguage;
            movieDetail.ReleaseDate = movie.ReleaseDate;
            movieDetail.RunTime = movie.RunTime;
            movieDetail.Price = movie.Price;
            movieDetail.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
                movieDetail.Genres.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });

            movieDetail.Casts = new List<CastResponseModel>();
            foreach (var mc in movie.MovieCasts)
                movieDetail.Casts.Add(new CastResponseModel
                {
                    Id = mc.Cast.Id,
                    Name = mc.Cast.Name,
                    Gender = mc.Cast.Gender,
                    TmdbUrl = mc.Cast.TmdbUrl,
                    ProfilePath = mc.Cast.ProfilePath,
                    Character = mc.Character
                });

            if (movie.Reviews.Any())
            {
                decimal rating = 0;
                foreach (var review in movie.Reviews) rating += review.Rating;

                movieDetail.Rating = rating / movie.Reviews.Count();
            }

            return movieDetail;
        }

        public async Task<IEnumerable<MovieDetailsResponse>> GetTopGrossingMovies()
        {
            var movieDetails = new List<MovieDetailsResponse>();
            var movies = await _repository.GetTopRevenueMovies();
            foreach (var movie in movies)
            {
                var movieCard = new MovieDetailsResponse
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Revenue = movie.Revenue,
                    Title = movie.Title
                };
                movieDetails.Add(movieCard);
            }

            return movieDetails;
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            return await _repository.GetTopRatedMovies();
        }

        public async Task<MovieDetailsResponse> BuyMovie(int id)
        {
            var movie = await _repository.GetByIdAsync(id);
            if (movie == null) return null;
            var purchase = new Purchase
            {
                UserId = _currentUser.UserId,
                TotalPrice = movie.Price,
                MovieId = id,
                PurchaseNumber = Guid.NewGuid()
            };

            await _purchaseRepository.AddAsync(purchase);

            if (purchase.Id > 0)
            {
                var movieDetail = new MovieDetailsResponse();
                movieDetail.Id = movie.Id;
                movieDetail.Title = movie.Title;
                movieDetail.Overview = movie.Overview;
                movieDetail.Tagline = movie.Tagline;
                movieDetail.Budget = movie.Budget;
                movieDetail.Revenue = movie.Revenue;
                movieDetail.ImdbUrl = movie.ImdbUrl;
                movieDetail.TmdbUrl = movie.TmdbUrl;
                movieDetail.PosterUrl = movie.PosterUrl;
                movieDetail.BackdropUrl = movie.BackdropUrl;
                movieDetail.OriginalLanguage = movie.OriginalLanguage;
                movieDetail.ReleaseDate = movie.ReleaseDate;
                movieDetail.RunTime = movie.RunTime;
                movieDetail.Price = movie.Price;
                movieDetail.Genres = new List<GenreModel>();
                foreach (var genre in movie.Genres)
                    movieDetail.Genres.Add(new GenreModel
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    });

                movieDetail.Casts = new List<CastResponseModel>();
                foreach (var mc in movie.MovieCasts)
                    movieDetail.Casts.Add(new CastResponseModel
                    {
                        Id = mc.Cast.Id,
                        Name = mc.Cast.Name,
                        Gender = mc.Cast.Gender,
                        TmdbUrl = mc.Cast.TmdbUrl,
                        ProfilePath = mc.Cast.ProfilePath,
                        Character = mc.Character
                    });

                if (movie.Reviews.Any())
                {
                    decimal rating = 0;
                    foreach (var review in movie.Reviews) rating += review.Rating;

                    movieDetail.Rating = rating / movie.Reviews.Count();
                }

                return movieDetail;
            }

            return null;
        }

        public async Task<bool> PostReview(ReviewRequestModel requestModel)
        {
            var dbReview = await _reviewRepository.GetByIdAsync(_currentUser.UserId, requestModel.MovieId);
            if (dbReview != null) return false;

            var review = new Review
            {
                MovieId = requestModel.MovieId,
                UserId = _currentUser.UserId,
                Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };

            await _reviewRepository.AddAsync(review);

            return true;
        }

        public async Task<PaginationResponse<MovieDetailsResponse>> GetMoviesPaginated(int pageNumber, int pageSize)
        {
            var paginatedMovies = await _repository.GetMoviesPaginated(pageNumber, pageSize);
            var movieResponses = new PaginationResponse<MovieDetailsResponse>
            {
                PageCount = paginatedMovies.PageCount,
                PageNumber = paginatedMovies.PageNumber,
                PageSize = paginatedMovies.PageSize,
                Data = new List<MovieDetailsResponse>()
            };

            foreach (var movie in paginatedMovies.Data)
            {
                var movieDetail = new MovieDetailsResponse();
                movieDetail.Id = movie.Id;
                movieDetail.Title = movie.Title;
                movieDetail.Overview = movie.Overview;
                movieDetail.Tagline = movie.Tagline;
                movieDetail.Budget = movie.Budget;
                movieDetail.Revenue = movie.Revenue;
                movieDetail.ImdbUrl = movie.ImdbUrl;
                movieDetail.TmdbUrl = movie.TmdbUrl;
                movieDetail.PosterUrl = movie.PosterUrl;
                movieDetail.BackdropUrl = movie.BackdropUrl;
                movieDetail.OriginalLanguage = movie.OriginalLanguage;
                movieDetail.ReleaseDate = movie.ReleaseDate;
                movieDetail.RunTime = movie.RunTime;
                movieDetail.Price = movie.Price;
                movieDetail.Genres = new List<GenreModel>();

                foreach (var genre in movie.Genres)
                    movieDetail.Genres.Add(new GenreModel
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    });

                movieDetail.Casts = new List<CastResponseModel>();
                foreach (var mc in movie.MovieCasts)
                    movieDetail.Casts.Add(new CastResponseModel
                    {
                        Id = mc.Cast.Id,
                        Name = mc.Cast.Name,
                        Gender = mc.Cast.Gender,
                        TmdbUrl = mc.Cast.TmdbUrl,
                        ProfilePath = mc.Cast.ProfilePath,
                        Character = mc.Character
                    });

                if (movie.Reviews.Any())
                {
                    decimal rating = 0;
                    foreach (var review in movie.Reviews) rating += review.Rating;

                    movieDetail.Rating = rating / movie.Reviews.Count();
                }

                movieResponses.Data.Add(movieDetail);
            }

            return movieResponses;
        }

        public async Task<PaginationResponse<MovieDetailsResponse>> GetMoviesByGenreId(int genreId, int pageNumber = 0,
            int pageSize = 30)
        {
            var movies = await _repository.GetMoviesByGenreId(genreId, pageNumber, pageSize);
            var movieResponses = new PaginationResponse<MovieDetailsResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = new List<MovieDetailsResponse>()
            };

            foreach (var movie in movies.Data)
            {
                var movieDetail = new MovieDetailsResponse();
                movieDetail.Id = movie.Id;
                movieDetail.Title = movie.Title;
                movieDetail.Overview = movie.Overview;
                movieDetail.Tagline = movie.Tagline;
                movieDetail.Budget = movie.Budget;
                movieDetail.Revenue = movie.Revenue;
                movieDetail.ImdbUrl = movie.ImdbUrl;
                movieDetail.TmdbUrl = movie.TmdbUrl;
                movieDetail.PosterUrl = movie.PosterUrl;
                movieDetail.BackdropUrl = movie.BackdropUrl;
                movieDetail.OriginalLanguage = movie.OriginalLanguage;
                movieDetail.ReleaseDate = movie.ReleaseDate;
                movieDetail.RunTime = movie.RunTime;
                movieDetail.Price = movie.Price;

                movieDetail.Genres = new List<GenreModel>();

                foreach (var genre in movie.Genres)
                    movieDetail.Genres.Add(new GenreModel
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    });
                
                movieDetail.Casts = new List<CastResponseModel>();
                foreach (var mc in movie.MovieCasts)
                    movieDetail.Casts.Add(new CastResponseModel
                    {
                        Id = mc.Cast.Id,
                        Name = mc.Cast.Name,
                        Gender = mc.Cast.Gender,
                        TmdbUrl = mc.Cast.TmdbUrl,
                        ProfilePath = mc.Cast.ProfilePath,
                        Character = mc.Character
                    });
                
                if (movie.Reviews.Any())
                {
                    decimal rating = 0;
                    foreach (var review in movie.Reviews) rating += review.Rating;
                
                    movieDetail.Rating = rating / movie.Reviews.Count();
                }

                movieResponses.Data.Add(movieDetail);
            }

            return movieResponses;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetReviewsByMovieId(int movieId)
        {
            var movie = await _repository.GetByIdAsync(movieId);
            var reviewResponses = new List<ReviewResponseModel>();

            foreach (var review in movie.Reviews)
                reviewResponses.Add(new ReviewResponseModel
                {
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });

            return reviewResponses;
        }
    }
}