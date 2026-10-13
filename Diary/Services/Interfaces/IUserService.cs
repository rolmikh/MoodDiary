using Diary.DTO;
using Diary.Models;

namespace Diary.Services.Interfaces
{
    public interface IUserService
    {

        Task<ServiceResult> Registration(UserRegistrationDTO user);

        Task<ServiceResult> Authorization(AuthorizationDTO user);
    }
}
