using AutoMapper;
using Cinema.Model;
using DTO.MovieModel;

namespace Cinema.Mapper;

public class MovieProfile: Profile
{
    public MovieProfile()
    {
        CreateMap<MoviePost, Movie>()
            .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UpdatedByUserId, opt => opt.MapFrom(src => src.UserId));
        CreateMap<Movie, MovieGet>();
        CreateMap<MoviePut, Movie>()
            .ForMember(dest => dest.UpdatedByUserId, opt => opt.MapFrom(src => src.UserId));
    }
    
}