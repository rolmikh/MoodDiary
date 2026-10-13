using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;
using Diary.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MoodDiaryDBContext _dbContext;
        private readonly IValidator<UserRegistrationDTO> _registration_validator;
        private readonly IValidator<AuthorizationDTO> _authorization_validator;
        

        public UserService(MoodDiaryDBContext context, IValidator<UserRegistrationDTO> registration_validator, IValidator<AuthorizationDTO> authorization_validator) 
        {
            _dbContext = context;
            _registration_validator = registration_validator;
            _authorization_validator = authorization_validator;
        
        }
        public async Task<ServiceResult> Registration(UserRegistrationDTO user)
        {

            var result = _registration_validator.Validate(user);

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

        public async Task<ServiceResult> Authorization(AuthorizationDTO user)
        {
            var result = _authorization_validator.Validate(user);

            if (!result.IsValid)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = string.Join("; ", result.Errors.Select(e => e.ErrorMessage))

                };
            }

            var searchUser = _dbContext.Users.Where(n => n.Email == user.Email && n.Password == user.Password);

            if (searchUser.Any()) 
            {
                return new ServiceResult
                {
                    IsSuccess = true
                };
            }
            
            return new ServiceResult
            {
                IsSuccess = false,
                Error = "Пользователя не существует"

            };

        }
    }
}
