namespace StoreApp.Features.Products.DTOs;

public record CategoryListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
}