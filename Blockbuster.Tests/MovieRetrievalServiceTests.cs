using AutoMapper;
using Blockbuster.Application.Movies.Services;
using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;
using Blockbuster.Infrastructure.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blockbuster.Tests;

public class MovieRetrievalServiceTests
{
    private readonly Mock<ICinemaWorldService> _cinemaWorldServiceMock;
    private readonly Mock<IFilmWorldService> _filmWorldServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<MovieRetrievalService>> _loggerMock;
    private readonly MovieRetrievalService _movieRetrievalService;


    public MovieRetrievalServiceTests()
    {
        _cinemaWorldServiceMock = new Mock<ICinemaWorldService>();
        _filmWorldServiceMock = new Mock<IFilmWorldService>();

        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<MovieRetrievalService>>();
        _movieRetrievalService = new MovieRetrievalService(_cinemaWorldServiceMock.Object, _filmWorldServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
    }


    [Fact]
    public async Task WhenCinemaWorldProviderFails_ResultsRetrievedFromFilmWorld()
    {

        var movieList = new List<Movie>()
        {
            new() {
                ID = "fw0076759",
                Title = "Star Wars: Episode IV - A New Hope",
                Year = "1977",
                Type = "movie"
            }
        };
        _cinemaWorldServiceMock.Setup(x => x.GetAllMoviesAsync()).ThrowsAsync(new Exception());

        _filmWorldServiceMock.Setup(x => x.GetAllMoviesAsync()).ReturnsAsync(movieList);

        _mapperMock.Setup(x => x.Map<List<MovieDto>>(movieList)).Returns([ new MovieDto() {
            ID = "fw0076759",
                Title = "Star Wars: Episode IV - A New Hope",
                Year = "1977",
                Type = "movie",
                MovieProvider = Domain.Enums.Enums.MovieProvider.FilmWorld
        } ]);

        var result = await _movieRetrievalService.GetMoviesAsync();

        result.Movies.Count.Should().Be(1);
        result.Movies.First().ID.Should().Be("fw0076759");
    }


    [Fact]
    public async Task WhenBothProviderFails_ResultsReturnsZeroMovies()
    {

        
        _cinemaWorldServiceMock.Setup(x => x.GetAllMoviesAsync()).ThrowsAsync(new Exception());

        _filmWorldServiceMock.Setup(x => x.GetAllMoviesAsync()).ThrowsAsync(new Exception());

        _mapperMock.Setup(x => x.Map<List<MovieDto>>(It.IsAny<List<Movie>>())).Returns(new List<MovieDto>());

        var result = await _movieRetrievalService.GetMoviesAsync();

        result.Movies.Count.Should().Be(0);
    }
}
