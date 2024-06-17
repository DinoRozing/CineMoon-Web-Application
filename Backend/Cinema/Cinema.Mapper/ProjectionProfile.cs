using AutoMapper;
using Cinema.Model;
using DTO.ProjectionModel;

namespace Cinema.Mapper
{
    public class ProjectionProfile : Profile
    {
        public ProjectionProfile()
        {
            CreateMap<Projection, GetProjectionRest>();
            CreateMap<PutProjectionRest, Projection>();
            CreateMap<PostProjectionRest, Projection>();
            CreateMap<PostProjectionHallRest, ProjectionHall>();
        }
    }
}
