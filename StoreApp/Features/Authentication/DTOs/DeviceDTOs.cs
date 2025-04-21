namespace StoreApp.Features.Authentication.DTOs;

public record DeviceCreateDto
{
  public required string FcmToken { get; set; }
}