using StoreApp.Core;

namespace StoreApp.Features.Authentication.Models;

public class Address : BaseModel
{
  public required int UserId { get; set; }
  public User User { get; set; }

  public required string Title { get; set; }
  public required string FullAddress { get; set; }
  public required double Lat { get; set; }
  public required double Lng { get; set; }
  public required bool IsDefault { get; set; }
}