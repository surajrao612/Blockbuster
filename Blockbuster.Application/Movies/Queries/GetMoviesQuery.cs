using Blockbuster.Application.Movies.TransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.Application.Movies.Queries;

public record GetMoviesQuery : IRequest<MovieResponse>
{
}
