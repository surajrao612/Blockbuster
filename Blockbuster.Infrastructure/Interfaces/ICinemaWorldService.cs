using Blockbuster.Domain.Entities;

namespace Blockbuster.Infrastructure.Interfaces;

public interface ICinemaWorldService
{
    Task<List<Movie>> GetAllMoviesAsync();


    Task<MovieInfo> GetMovieInfoAsync(string id);
}
