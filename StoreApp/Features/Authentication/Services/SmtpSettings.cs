namespace StoreApp.Features.Authentication.Services;

public class SmtpSettings
{
  public required string Host { get; set; }
  public required int Port { get; set; }
  public required bool EnableSsl { get; set; }
  public required string UserName { get; set; }
  public required string Password { get; set; }
}