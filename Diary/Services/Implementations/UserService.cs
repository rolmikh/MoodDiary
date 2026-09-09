using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;

namespace Diary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MoodDiaryDBContext _dbContext;

        public UserService(MoodDiaryDBContext context) 
        {
            _dbContext = context;
        
        }
        public async Task<User> Registration(UserRegistrationDTO user)
        {
            User newUser = new User(user.Email, user.Password);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        //public async Task<> Authorization(UserRegistrationDTO user)
        //{
            

        //    return newUser;
        //}
    }
}
