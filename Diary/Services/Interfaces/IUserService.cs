using Diary.DTO;
using Diary.Models;

namespace Diary.Services.Interfaces
{
    public interface IUserService
    {

        Task<ServiceResult> Registration(UserRegistrationDTO user);

        Task<ServiceResult> Authorization(AuthorizationDTO user);

        string GenerateCode();

        Task<ServiceResult> SendConfirmationCode(string email);

        Task<bool> SaveCodeToDatabase(string code, string email);

        Task<ServiceResult> ConfirmEmail(string code, string email);
    }
}
