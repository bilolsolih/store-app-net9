using StoreApp.Core;
using StoreApp.Features.Authentication.Models;

namespace StoreApp.Features.Reviews.Models;

public class Review : BaseModel
{
  public required string Comment { get; set; }
  public required int Rating { get; set; }
  public required int UserId { get; set; }
  public User User { get; set; }
  public required int ProductId { get; set; }
}