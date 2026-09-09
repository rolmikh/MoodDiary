using Diary.DTO;
using Diary.Models;

namespace Diary.Services.Interfaces
{
    public interface IUserService
    {

        Task<User> Registration(UserRegistrationDTO user);

        //Task<User> Authorization(UserRegistrationDTO user);
    }
}
