using AutoMapper;
using StoreApp.Features.Products.DTOs;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Profiles;

public class CategoryProfiles : Profile
{
  public CategoryProfiles()
  {
    CreateMap<Category, CategoryListDto>();
  }
}