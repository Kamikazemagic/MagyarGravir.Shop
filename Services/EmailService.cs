using System.Net;
using System.Net.Mail;

namespace MagyarGravir.Shop.Services;

public class EmailService
{
    public async Task SendEmailAsync(string to, string subject, string html)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("you@gmail.com", "app-password"),
            EnableSsl = true
        };

        var mail = new MailMessage("you@gmail.com", to, subject, html)
        {
            IsBodyHtml = true
        };

        await client.SendMailAsync(mail);
    }
}