using Blockbuster.Application.Interfaces;
using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
