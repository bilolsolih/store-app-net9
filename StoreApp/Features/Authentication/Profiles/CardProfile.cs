using AutoMapper;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Profiles;

public class CardProfile : Profile
{
  public CardProfile()
  {
    CreateMap<Card, CardListDto>();
  }
}