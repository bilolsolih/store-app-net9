namespace StoreApp.Features.Authentication.Models;

public class Otp
{
  public int Id { get; set; }
  public required int UserId { get; set; }
  public required string Code { get; set; }
  public DateTime ExpiryDate { get; set; }

  public required User User { get; set; }

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}