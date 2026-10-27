using Diary.Services.Interfaces;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Diary.Settings;
using Microsoft.Extensions.Options;


namespace Diary.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _settings;

        public EmailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendEmailMessage(string to, string body, string subject)
        {

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.Sender, _settings.UserName));
                message.To.Add(new MailboxAddress("", to));

                message.Subject = subject;

                message.Body = new TextPart("plain")
                {
                    Text = body
                };
                using (var client = new SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect);
                        await client.AuthenticateAsync(_settings.UserName, _settings.Password);
                        await client.SendAsync(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }
                    finally
                    {
                        await client.DisconnectAsync(true);
                        client.Dispose();

                    }
                }

                return true;
            }
            catch (Exception)
            { 
                return false;
           
            }
            

        }
    }
}
