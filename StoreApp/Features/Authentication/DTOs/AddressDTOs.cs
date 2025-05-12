namespace StoreApp.Features.Authentication.DTOs;

public record AddressCreateDto
{
  public required string Title { get; set; }
  public required string FullAddress { get; set; }
  public required double Lat { get; set; }
  public required double Lng { get; set; }
  public required bool IsDefault { get; set; }
}

public record AddressListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string FullAddress { get; set; }
  public required double Lat { get; set; }
  public required double Lng { get; set; }
  public required bool IsDefault { get; set; }
}