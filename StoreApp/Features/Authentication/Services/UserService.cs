using System.Text;
using AutoMapper;
using StoreApp.Core;
using StoreApp.Core.Exceptions;
using StoreApp.Features.Authentication.DTOs;
using StoreApp.Features.Authentication.Models;
using StoreApp.Features.Authentication.Repositories;

namespace StoreApp.Features.Authentication.Services;

public class UserService(
  UserRepository userRepo,
  OtpRepository otpRepo,
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
    var user = await userRepo.GetByEmailAsync(value);
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

  public async Task SendOtpToEmailAsync(SendOtpDto payload)
  {
    var user = await userRepo.GetByEmailAsync(payload.Email);
    if (user == null) return;

    var oldOtps = await otpRepo.GetAllByUserEmailAsync(user.Email);
    await otpRepo.DeleteAllAsync(oldOtps);

    var code = Random.Shared.Next(minValue: 0, maxValue: 9999).ToString();
    var otpCode = new StringBuilder();
    for (int i = 0; i < 4 - code.Length; i++)
    {
      otpCode.Append('0');
    }

    otpCode.Append(code);

    var newOtp = new Otp
    {
      UserId = user.Id,
      User = user,
      Code = otpCode.ToString(),
      ExpiryDate = DateTime.UtcNow.AddMinutes(10)
    };

    await otpRepo.AddOtpAsync(newOtp);
  }

  public async Task<bool> VerifyOtpAsync(VerifyOtpDto payload)
  {
    var otpCode = await otpRepo.GetByEmailAndCodeAsync(payload.Email, payload.Code);
    DoesNotExistException.ThrowIfNull(otpCode, payload.ToString());

    if (otpCode.ExpiryDate >= DateTime.UtcNow)
    {
      return true;
    }

    return false;
  }

  public async Task<User> ResetPasswordAsync(ResetPasswordDto payload)
  {
    var user = await userRepo.GetByEmailAsync(payload.Email);
    DoesNotExistException.ThrowIfNull(user, payload.ToString());

    var otp = await otpRepo.GetByEmailAndCodeAsync(payload.Email, payload.Code);
    DoesNotExistException.ThrowIfNull(otp, payload.ToString());

    if (otp.ExpiryDate < DateTime.UtcNow)
    {
      throw new OtpCodeExpiredException();
    }

    user.Password = payload.Password;
    await userRepo.UpdateAsync(user);
    return user;
  }
}