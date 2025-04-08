using AutoMapper;
using StoreApp.Features.Products.DTOs;
using StoreApp.Features.Products.Models;

namespace StoreApp.Features.Products.Profiles;

public class ProductProfiles : Profile
{
  public ProductProfiles()
  {
    CreateMap<Product, ProductListDto>()
      .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.ProductImages.Single(pi => pi.IsMain).Image));
  }
}