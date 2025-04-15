namespace StoreApp.Features.Products.Filters;

public class ProductFilters
{
  public string? Title { get; set; }
  public int? CategoryId { get; set; }
  public int? SizeId { get; set; }
  public double? MinPrice { get; set; }
  public double? MaxPrice { get; set; }
  public string? OrderBy { get; set; }
}