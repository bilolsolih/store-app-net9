using System.ComponentModel.DataAnnotations;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Authentication.DTOs;

public record LoginDto
{
  [MaxLength(64)]
  public required string Login { get; set; }

  [MaxLength(64)]
  public required string Password { get; set; }
}

public record UserCreateDto
{
  public required string FullName { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
}

public record UserDetailDto
{
  public required int Id { get; set; }
  public required string FullName { get; set; }
  public required string Email { get; set; }
  public string? PhoneNumber { get; set; }
  public Gender? Gender { get; set; }
  public DateOnly? BirthDate { get; set; }
}

public record UserUpdateDto
{
  public Gender? Gender { get; set; }
  public string? FullName { get; set; }
  public string? Email { get; set; }
  public string? PhoneNumber { get; set; }
  public string? BirthDate { get; set; }
}

public record UserListDto
{
  public required int Id { get; set; }
  public required string FullName { get; set; }
  public required string Email { get; set; }
  public string? PhoneNumber { get; set; }
  public Gender? ProfilePhoto { get; set; }
}

public record SendOtpDto
{
  public required string Email { get; set; }
}

public record VerifyOtpDto
{
  public required string Email { get; set; }
  public required string Code { get; set; }
}

public record ResetPasswordDto
{
  public required string Email { get; set; }
  public required string Code { get; set; }
  public required string Password { get; set; }
}