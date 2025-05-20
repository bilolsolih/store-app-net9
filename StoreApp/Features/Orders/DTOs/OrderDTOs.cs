using StoreApp.Features.Orders.Models;

namespace StoreApp.Features.Orders.DTOs;

public record OrderCreateDto
{
  public required int AddressId { get; set; }
  public required PaymentMethods PaymentMethod { get; set; }
  public int? CardId { get; set; }
}

public record OrderListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string? Image { get; set; }
  public required string Size { get; set; }
  public required double Price { get; set; }
  public required OrderStatus Status { get; set; }
  public int? Rating { get; set; }
}