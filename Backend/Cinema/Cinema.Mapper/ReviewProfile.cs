﻿using AutoMapper;
using Cinema.Model;
using DTO;
using DTO.ReviewModel;

namespace Cinema.Mapper
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, GetReviewRest>();
            CreateMap<PostReviewRest, Review>();
            CreateMap<PutReviewRest, Review>();
            CreateMap<Review, GetReviewRest>().ReverseMap();
        }
    }
}
