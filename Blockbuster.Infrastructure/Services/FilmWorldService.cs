using Blockbuster.Domain.Entities;
using Blockbuster.Infrastructure.Configuration;
using Blockbuster.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.Infrastructure.Services;

public class FilmWorldService : IFilmWorldService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger _logger;
    private readonly BlockbusterConfig _config;

    private const string MOVIESKEY = "FilmWorld";

    public FilmWorldService(HttpClient httpClient, IMemoryCache memoryCache, ILogger<FilmWorldService> logger, IOptions<BlockbusterConfig> options)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _logger = logger;
        _config = options.Value;
    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        try
        {
            
            _httpClient.DefaultRequestHeaders.Add("x-access-token", _config.ApiAccessToken);

            var movieResponse = await _httpClient.GetFromJsonAsync<MoviesResult>("movies");



            if (movieResponse?.Movies?.Count > 0)
            {
                _memoryCache.Set(MOVIESKEY, movieResponse.Movies, TimeSpan.FromMinutes(_config.DataCacheInMinutes));

                return movieResponse.Movies;
            }


            //try to return cached result if api fails
            var cachedResult = _memoryCache.Get(MOVIESKEY);


            if (cachedResult != null)
                return (List<Movie>)cachedResult;



        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching movies from FilmWorld: {Message}", ex.Message);
        }

        return [];

    }

    public async Task<MovieInfo> GetMovieInfoAsync(string id)
    {
        try
        {
            string cacheName = $"{MOVIESKEY}_{id}";
            


            _httpClient.DefaultRequestHeaders.Add("x-access-token", _config.ApiAccessToken);

            var movieResponse = await _httpClient.GetFromJsonAsync<MovieInfo>($"movie/{id}");



            if (movieResponse != null)
            {
                _memoryCache.Set(cacheName, movieResponse, TimeSpan.FromMinutes(_config.DataCacheInMinutes));

                return movieResponse;
            }

            //try to return cached result if api fails
            var cachedResult = _memoryCache.Get(cacheName);


            if (cachedResult != null)
                return (MovieInfo)cachedResult;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching movie details from FilmWorld: {Message}", ex.Message);
        }

        return new();
    }
}
