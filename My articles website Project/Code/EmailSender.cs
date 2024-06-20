using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace My_articles_website_Project.Code
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpclient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("oussama.souissi024@gmail.com", "sfjyzzzjeawsqzgn"),
                EnableSsl = true
            };

            return smtpclient.SendMailAsync("oussama.souissi024@gmail.com", email, subject, htmlMessage);
        }
    }
}
