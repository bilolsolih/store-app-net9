using AutoMapper;
using StoreApp.Features.Cart.DTOs;
using StoreApp.Features.Cart.Models;

namespace StoreApp.Features.Cart.Profiles;

public class CartItemProfile : Profile
{
  public CartItemProfile()
  {
    CreateMap<CartItem, CartItemListDto>()
      .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Product.Title))
      .ForMember(dest => dest.Size, opts => opts.MapFrom(src => src.Size.Title))
      .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Product.Price))
      .ForMember(
        dest => dest.Image,
        opts => opts.MapFrom(src => src.Product.ProductImages.Single(p => p.IsMain).Image)
      );
  }
}