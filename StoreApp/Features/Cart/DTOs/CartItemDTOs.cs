namespace StoreApp.Features.Cart.DTOs;

public record CartItemCreateDto
{
  public required int ProductId { get; set; }
  public required int SizeId { get; set; }
}

public record CartItemListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Size { get; set; }
  public required double Price { get; set; }
  public required string Image { get; set; }
  public required int Quantity { get; set; }
}