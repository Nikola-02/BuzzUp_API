using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Utilities
{
    public class ForgotPassword
    {
        public static string GenerateToken(int length = 64)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(length);
            return Convert.ToBase64String(randomBytes);
        }
        public static void SendEmail(string toEmail, string link, string smtpEmail, string smtpPassword)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("BuzzUp", "no-reply@buzzup.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Reset Password";

            message.Body = new TextPart("html")
            {
                Text = $"Click <a href='{link}'>here</a> to reset your password. Link will expire in 1 hour"
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(smtpEmail, smtpPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
