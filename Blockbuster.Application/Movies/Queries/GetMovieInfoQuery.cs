using Blockbuster.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blockbuster.Domain.Enums.Enums;

namespace Blockbuster.Application.Movies.Queries
{
    public class GetMovieInfoQuery : IRequest<MovieInfo>
    {
        public string Id { get; set; } = string.Empty;

    }
}
