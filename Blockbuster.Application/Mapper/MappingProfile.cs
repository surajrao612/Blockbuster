using AutoMapper;
using Blockbuster.Application.Movies.TransferObjects;
using Blockbuster.Domain.Entities;
using static Blockbuster.Domain.Enums.Enums;

namespace Blockbuster.Application.Mapper;

public class MappingProfile : Profile
{
    

    public MappingProfile()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(dst => dst.MovieProvider, cfg => cfg.MapFrom(src => src.ID.StartsWith(Constants.CinemWorldPrefix, StringComparison.OrdinalIgnoreCase) ? MovieProvider.CinemaWorld : MovieProvider.FilmWorld));

        CreateMap<MovieInfo, MovieInfoResponse>()
            .ForMember(dst => dst.MovieProvider, cfg => cfg.MapFrom(src => src.ID.StartsWith(Constants.CinemWorldPrefix, StringComparison.OrdinalIgnoreCase) ? MovieProvider.CinemaWorld : MovieProvider.FilmWorld));
    }
    
}
