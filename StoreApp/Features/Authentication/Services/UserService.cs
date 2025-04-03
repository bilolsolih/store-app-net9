using AutoMapper;
using StoreApp.Core;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Filters;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Authentication.Repositories;

namespace StoreApp.Features.Authentication.Services;

public class UserService(
  UserRepository userRepo,
  IMapper mapper,
  IWebHostEnvironment webEnv,
  IHttpContextAccessor httpContextAccessor
) : ServiceBase("profiles", webEnv, httpContextAccessor)
{
  public async Task<User> CreateUserAsync(UserCreateDto payload)
  {
    var user = mapper.Map<User>(payload);

    return await userRepo.AddAsync(user);
  }


  public async Task<UserDetailDto> GetUserByIdAsync(int id)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");

    return mapper.Map<UserDetailDto>(user);
  }

  public async Task<User?> GetUserByLoginAsync(string value)
  {
    var user = await userRepo.GetUserByLoginAsync(value);
    return user;
  }

  public async Task<UserDetailDto> UpdateUserAsync(int id, UserUpdateDto payload)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
    mapper.Map(payload, user);
    user = await userRepo.UpdateAsync(user);

    return mapper.Map<UserDetailDto>(user);
  }
  
  
}