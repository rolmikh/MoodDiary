using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;
using Diary.Validators;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Diary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MoodDiaryDBContext _dbContext;

        public UserService(MoodDiaryDBContext context) 
        {
            _dbContext = context;
        
        }
        public async Task<bool> Registration(UserRegistrationDTO user)
        {
            var findUser = _dbContext.Users.Where(n => n.Email == user.Email);
            if (findUser.Any())
            {
                return false;
            }
            
            User newUser = new User(user.Email, user.Password);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Authorization(AuthorizationDTO user)
        {
            var searchUser = _dbContext.Users.Where(n => n.Email == user.Email && n.Password == user.Password);

            if (searchUser.Any()) 
            {
                return true;
            }
            return false;

        }
    }
}
