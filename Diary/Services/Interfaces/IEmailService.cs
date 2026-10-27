namespace Diary.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailMessage(string to, string body, string subject);
    }
}
