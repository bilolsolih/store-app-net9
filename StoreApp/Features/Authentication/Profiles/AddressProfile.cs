using AutoMapper;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.Profiles;

public class AddressProfile : Profile
{
  public AddressProfile()
  {
    CreateMap<AddressCreateDto, Address>();
    CreateMap<Address, AddressListDto>();
  }
}