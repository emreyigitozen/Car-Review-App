using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Models;

namespace CarReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CarDto, Car>();
            CreateMap<Car, CarDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<ReviewDto,Review>();
            CreateMap<Review,ReviewDto>();
            CreateMap<Reviewer,ReviewerDto>();
            CreateMap<ReviewerDto,Reviewer>();

        }



    }
}
