using Blockbuster.Application.Interfaces;
using Blockbuster.Application.Movies.TransferObjects;
using MediatR;

namespace Blockbuster.Application.Movies.Queries;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, MovieResponse>
{

    private readonly IMovieRetrievalService _movieRetrievalService;


    public GetMoviesQueryHandler(IMovieRetrievalService movieRetrievalService)
    {
        _movieRetrievalService = movieRetrievalService;
    }


    public async Task<MovieResponse> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _movieRetrievalService.GetMoviesAsync();
    }
}
