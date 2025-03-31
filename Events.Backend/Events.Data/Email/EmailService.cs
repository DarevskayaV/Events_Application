using System.Net.Mail;
using System.Net;
using Events.Application.Interfaces;

namespace Events.Events.Data.Email
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            //Настройка SMTP-клиента
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("empireofdreams8@gmail.com", "qnebfhhyehqrlqao"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("empireofdreams8@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };
            mailMessage.To.Add(to);

            //отправка почты
            return smtpClient.SendMailAsync(mailMessage);
        }
    }
}

