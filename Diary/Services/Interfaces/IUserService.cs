using Diary.DTO;
using Diary.Models;

namespace Diary.Services.Interfaces
{
    public interface IUserService
    {

        Task<bool> Registration(UserRegistrationDTO user);

        Task<bool> Authorization(AuthorizationDTO user);
    }
}
