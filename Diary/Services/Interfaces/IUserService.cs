using Diary.DTO;
using Diary.Models;

namespace Diary.Services.Interfaces
{
    public interface IUserService
    {

        Task<ServiceResult> Registration(UserRegistrationDTO user);

        Task<ServiceResult> Authorization(AuthorizationDTO user);

        string GenerateCode();

        Task<bool> SendConfirmationCode(string email);

        Task<bool> SaveCodeToDatabase(string code, string email);

        Task<bool> ConfirmEmail(string code, string email);
    }
}
