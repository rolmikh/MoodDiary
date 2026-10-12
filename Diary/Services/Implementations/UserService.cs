using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;
using Diary.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MoodDiaryDBContext _dbContext;

        public UserService(MoodDiaryDBContext context) 
        {
            _dbContext = context;
        
        }
        public async Task<ServiceResult> Registration(UserRegistrationDTO user)
        {

            UserRegistrationValidator validations = new UserRegistrationValidator();
            ValidationResult result = validations.Validate(user);

            if (!result.IsValid)
            {
                return new ServiceResult 
                { 
                    IsSuccess = false,
                    Error = string.Join("; ", result.Errors.Select(e => e.ErrorMessage))
                
                };
            }
            var findUser = _dbContext.Users.Where(n => n.Email == user.Email);
            if (findUser.Any())
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Пользователь с таким Email уже существует"

                };
            }
            
            User newUser = new User(user.Email, user.Password);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true
            };
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
