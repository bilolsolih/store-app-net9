using System.Text.Json.Serialization;

namespace StoreApp.Features.Products.Models;

public class ProductImage
{
  public int Id { get; set; }
  public required string Image { get; set; }
  public required bool IsMain { get; set; }

  [JsonIgnore]
  public required Product Product { get; set; }

  public required int ProductId { get; set; }

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}