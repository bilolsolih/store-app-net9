using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace StoreApp.Features.Authentication.Services;

public class MailKitEmailSender(IOptions<SmtpSettings> smtpOptions) : IEmailSender
{
  private readonly SmtpSettings _settings = smtpOptions.Value;

  public async Task SendEmailAsync(string to, string subject, string body)
  {
    var email = new MimeMessage();
    email.From.Add(MailboxAddress.Parse(_settings.UserName));
    email.To.Add(MailboxAddress.Parse(to));
    email.Subject = subject;

    var builder = new BodyBuilder() { HtmlBody = body };
    email.Body = builder.ToMessageBody();

    using var smtp = new SmtpClient();
    await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
    await smtp.AuthenticateAsync(_settings.UserName, _settings.Password);
    await smtp.SendAsync(email);
    await smtp.DisconnectAsync(true);
  }
}