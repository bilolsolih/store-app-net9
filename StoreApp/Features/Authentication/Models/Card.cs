using StoreApp.Core;

namespace StoreApp.Features.Authentication.Models;

public class Card : BaseModel
{
  public required int UserId { get; set; }
  public User User { get; set; }

  public required string CardNumber { get; set; }
  public required DateOnly ExpiryDate { get; set; }
  public required string SecurityCode { get; set; }
}