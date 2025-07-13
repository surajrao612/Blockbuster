using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;

namespace Blockbuster.Application.Interfaces;

public interface IMovieRetrievalService
{
    
    Task<MovieResponse> GetMoviesAsync();

    
    Task<MovieInfo> GetMovieInfoByIdAsync(string id);

}
