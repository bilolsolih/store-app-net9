namespace StoreApp.Features.Authentication.Services;

public interface IEmailSender
{
  Task SendEmailAsync(string to, string subject, string body);
}