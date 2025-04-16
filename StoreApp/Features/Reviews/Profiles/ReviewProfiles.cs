using AutoMapper;
using StoreApp.Features.Reviews.DTOs;
using StoreApp.Features.Reviews.Models;

namespace StoreApp.Features.Reviews.Profiles;

public class ReviewProfiles : Profile
{
  public ReviewProfiles()
  {
    CreateMap<Review, ReviewListDto>()
      .ForMember(dest => dest.UserFullName, opts => opts.MapFrom(src => src.User.FullName));

    CreateMap<ReviewCreateDto, Review>();
  }
}