using Artifactan.Config;
using Microsoft.Extensions.Options;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Artifactan.Utils;

public class SendEmailUtils
{
    private readonly SendEmailConfig _sendEmailConfig;

    public SendEmailUtils(IOptions<SendEmailConfig> sendEmailConfig)
    {
        _sendEmailConfig = sendEmailConfig.Value;
    }
    
    public void Send(string to, string subject, string html, string? from = null)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _sendEmailConfig.EmailFrom));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };
        
        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_sendEmailConfig.SmtpHost, _sendEmailConfig.SmtpPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_sendEmailConfig.SmtpUser, _sendEmailConfig.SmtpPass);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}