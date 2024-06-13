using AutoMapper;
using Cinema.Model;
using DTO;

namespace Cinema.Mapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewRest>().ReverseMap();
        }
    }
}
