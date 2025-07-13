using Blockbuster.Application.Interfaces;
using Blockbuster.Domain.Entities;
using MediatR;

namespace Blockbuster.Application.Movies.Queries;

public class GetMovieInfoQueryHandler : IRequestHandler<GetMovieInfoQuery, MovieInfo>
{
    private readonly IMovieRetrievalService _movieRetrievalService;


    public GetMovieInfoQueryHandler(IMovieRetrievalService movieRetrievalService)
    {
        _movieRetrievalService = movieRetrievalService;
    }


    public async Task<MovieInfo> Handle(GetMovieInfoQuery request, CancellationToken cancellationToken)
    {
        return await _movieRetrievalService.GetMovieInfoByIdAsync(request.Id);
    }
}
