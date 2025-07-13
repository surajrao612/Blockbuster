using Blockbuster.Application.Movies.TransferObjects;
using System.Text.Json;

namespace Blockbuster.AcceptanceTests.StepDefinitions;

[Binding]
public class BlockbusterStepDefinitions
{
    private readonly TestContext _testContext;
    public BlockbusterStepDefinitions(TestContext testContext)
    {

        _testContext = testContext;
        
    }

    [Given(@"User wants to get all movies")]
    public void GivenUserWantsToGetAllMovies()
    {
        var httpClient = _testContext.ApplicationFactory.CreateClient();
        _testContext.Client = httpClient;

    }

    [When(@"Performs an all movies search")]
    public void WhenPerformsAnAllMoviesSearch()
    {

        var httpResponseMessage = _testContext.Client.GetAsync("/api/Blockbuster/movies").Result;
        _testContext.ResponseMessage = httpResponseMessage;
    }

    [Then(@"User receives the movies list")]
    public void ThenUserReceivesTheMoviesList()
    {
        var stringResponse = _testContext.ResponseMessage.Content.ReadAsStringAsync().Result;

        var allMovies = JsonSerializer.Deserialize<MovieResponse>(stringResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        allMovies.Should().NotBeNull();
        allMovies.Movies.Count.Should().BeGreaterThanOrEqualTo(0);

    }

    [Given(@"User wants to get movie details with a valid id")]
    public void GivenUserWantsToGetMovieDetailsWithAValidId()
    {
        _testContext.MovieId = "cw0076759";
    }

    [When(@"Performs an get movie info search")]
    public async void WhenPerformsAnGetMovieInfoSearch()
    {

        _testContext.Client = _testContext.ApplicationFactory.CreateClient();

        var httpResponseMessage = _testContext.Client.GetAsync($"/api/Blockbuster/movie/{_testContext.MovieId}").Result;
        _testContext.ResponseMessage = httpResponseMessage;

    }

    [Then(@"User receives the movie info")]
    public void ThenUserReceivesTheMovieInfo()
    {
        var stringResponse = _testContext.ResponseMessage.Content.ReadAsStringAsync().Result;

        var moviesInfo = JsonSerializer.Deserialize<MovieInfoResponse>(stringResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        moviesInfo.Should().NotBeNull();
        moviesInfo.ID.Should().NotBeNullOrEmpty();
        moviesInfo.ID.Should().Be(_testContext.MovieId);
    }

    [Given(@"User wants to get movie details with an invalid id")]
    public void GivenUserWantsToGetMovieDetailsWithAnInvalidId()
    {
        _testContext.MovieId = "aa0080684";
    }

    [Then(@"User receives not found")]
    public void ThenUserReceivesNotFound()
    {
        _testContext.ResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
