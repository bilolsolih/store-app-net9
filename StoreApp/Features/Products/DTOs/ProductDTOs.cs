namespace StoreApp.Features.Products.DTOs;

public record ProductListDto
{
  public required int Id { get; set; }
  public string? Image { get; set; }
  public required string Title { get; set; }
  public required double Price { get; set; }
  public required bool IsLiked { get; set; }
  public required double Discount { get; set; }
}