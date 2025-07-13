using AutoMapper;
using Blockbuster.Application.Interfaces;
using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;
using Blockbuster.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using static Blockbuster.Domain.Enums.Enums;

namespace Blockbuster.Application.Movies.Services;

public class MovieRetrievalService : IMovieRetrievalService
{
    private readonly ICinemaWorldService _cinemaWorldService;
    private readonly IFilmWorldService _filmWorldService;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public MovieRetrievalService(ICinemaWorldService cinemaWorldService, IFilmWorldService filmWorldService, IMapper mapper, ILogger<MovieRetrievalService> logger)
    { 
        _cinemaWorldService = cinemaWorldService;
        _filmWorldService = filmWorldService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<MovieResponse> GetMoviesAsync()
    {
        MovieResponse movieResponse = new();

        try
        {
            //cinemaworld task
            async Task<List<Movie>> GetCinemaWorldMoviesAsync()
            {
                try
                {
                    return await _cinemaWorldService.GetAllMoviesAsync();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error from provider CinemaWorld: {Message}", ex.Message);
                    return [];
                }
                

            }


            //filmworldtask
            async Task<List<Movie>> GetFilmWorldMoviesAsync()
            {

                try
                {
                    return await _filmWorldService.GetAllMoviesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error from provider FilmWorld: {Message}", ex.Message);
                    return [];
                }
            }



            var fetchCinemaWorldMovies = GetCinemaWorldMoviesAsync();
            var fetchFilmWorldMovies = GetFilmWorldMoviesAsync();


            await Task.WhenAll(fetchCinemaWorldMovies, fetchFilmWorldMovies);


            var response = MergeProviderResults(fetchCinemaWorldMovies.Result, fetchFilmWorldMovies.Result);

            return response;

        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error fetching movies from providers");
            return movieResponse;
        }
        
    }

    private MovieResponse MergeProviderResults(List<Movie> cwMovies, List<Movie> fwMovies)
    {
        var movieList = cwMovies
            .Concat(fwMovies)
            .GroupBy(x => x.Title)
            .Select(y => y.First())
            .ToList();

        var moviesDto = _mapper.Map<List<MovieDto>>(movieList);

        return new MovieResponse
        {
            Movies = moviesDto
        };
    }



    public async Task<MovieInfo> GetMovieInfoByIdAsync(string id)
    {
        MovieInfo? response = new();

        MovieProvider provider = id.StartsWith(Constants.CinemWorldPrefix, StringComparison.OrdinalIgnoreCase) ? MovieProvider.CinemaWorld : MovieProvider.FilmWorld;

        try
        {

            response = provider switch
            {
                MovieProvider.CinemaWorld => await _cinemaWorldService.GetMovieInfoAsync(id),
                MovieProvider.FilmWorld => await _filmWorldService.GetMovieInfoAsync(id),
                _ => new(),
            };

            var titleFromCompetitor = await CompareWithProviderAsync(provider, response.Title);

            if (titleFromCompetitor != null && !string.IsNullOrEmpty(titleFromCompetitor.Price) 
                && titleFromCompetitor.Price?.ConvertToDecimal() < response.Price?.ConvertToDecimal())
            {
                return titleFromCompetitor;
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while trying to get movie details from provider {provider}, Error: {Message}", provider.ToString(), ex.Message);            
        }

        return response;

    }

    private async Task<MovieInfo> CompareWithProviderAsync(MovieProvider provider, string title)
    {
        List<Movie> movies = [];
        var providerToCompare = GetProviderToCompare(provider);

        if (providerToCompare == MovieProvider.CinemaWorld)
            movies = await _cinemaWorldService.GetAllMoviesAsync();

        if (providerToCompare == MovieProvider.FilmWorld)
            movies = await _filmWorldService.GetAllMoviesAsync();

        var competitorMovie = movies.Where(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        if (competitorMovie == null)
            return default;

        var movieInfo = providerToCompare switch
            {
                MovieProvider.CinemaWorld => await _cinemaWorldService.GetMovieInfoAsync(competitorMovie.ID),
                MovieProvider.FilmWorld => await _filmWorldService.GetMovieInfoAsync(competitorMovie.ID),
                _ => new(),
            };

        return movieInfo;

    }

    private MovieProvider GetProviderToCompare(MovieProvider provider)
    {
        return provider switch
        {
            MovieProvider.CinemaWorld => MovieProvider.FilmWorld,
            MovieProvider.FilmWorld => MovieProvider.CinemaWorld,
            _ => provider,
        };
    }

    

}
