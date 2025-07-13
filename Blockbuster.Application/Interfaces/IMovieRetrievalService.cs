using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blockbuster.Domain.Enums.Enums;

namespace Blockbuster.Application.Interfaces;

public interface IMovieRetrievalService
{
    
    Task<MovieResponse> GetMoviesAsync();

    
    Task<MovieInfo> GetMovieInfoByIdAsync(string id);

}
