using Blockbuster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockbuster.Infrastructure.Interfaces;

public interface IFilmWorldService
{
    Task<List<Movie>> GetAllMoviesAsync();

    Task<MovieInfo> GetMovieInfoAsync(string id);
}
