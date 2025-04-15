using AutoMapper;
using StoreApp.Features.Products.DTOs;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Profiles;

public class SizeProfiles : Profile
{
  public SizeProfiles()
  {
    CreateMap<Size, SizeListDto>();
  }
}