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
    CreateMap<ProductImage, Picture>();
    CreateMap<Size, ProductSize>();
    CreateMap<Product, ProductDetailDto>()
      .ForMember(dest => dest.ReviewsCount, opts => opts.MapFrom(src => src.Reviews.Count))
      .ForMember(
        dest => dest.ProductSizes,
        opts => opts.MapFrom(src => src.Sizes.Select(size => new ProductSize { Id = size.Id, Title = size.Title }))
      )
      .ForMember(
        dest => dest.Rating,
        opts => opts.MapFrom(src => src.Reviews.Count > 0 ? (double)src.Reviews.Sum(r => r.Rating) / src.Reviews.Count : 0)
      );
  }
}