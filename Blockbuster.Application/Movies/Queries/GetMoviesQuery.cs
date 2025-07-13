using Blockbuster.Application.Movies.TransferObjects;
using MediatR;

namespace Blockbuster.Application.Movies.Queries;

public record GetMoviesQuery : IRequest<MovieResponse>
{
}
