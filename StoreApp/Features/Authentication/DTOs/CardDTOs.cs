namespace StoreApp.Features.Authentication.DTOs;

public class CardCreateDto
{
  public required string CardNumber { get; set; }
  public required DateOnly ExpiryDate { get; set; }
  public required string SecurityCode { get; set; }
}


public class CardListDto
{
  public required int Id { get; set; }
  public required string CardNumber { get; set; }
}