using Blockbuster.Domain.Entities;
using MediatR;

namespace Blockbuster.Application.Movies.Queries
{
    public class GetMovieInfoQuery : IRequest<MovieInfo>
    {
        public string Id { get; set; } = string.Empty;

    }
}
