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

        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(MoodDiaryDBContext context, IValidator<UserRegistrationDTO> registration_validator, IValidator<AuthorizationDTO> authorization_validator, PasswordHasher<User> passwordHasher) 
        {
            _dbContext = context;
            _registration_validator = registration_validator;
            _authorization_validator = authorization_validator;
            _passwordHasher = passwordHasher;
        
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
            
            User newUser = new User(user.Email);

            string passwordHash = _passwordHasher.HashPassword(newUser, user.Password);

            newUser.SetPasswordHash(passwordHash);

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

            var foundUser =  await _dbContext.Users.FirstOrDefaultAsync(n => n.Email == user.Email);

            if (foundUser == null) 
            {

                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Неверный Email"

                };
 
            }

            var result_password = _passwordHasher.VerifyHashedPassword(foundUser, foundUser.Password, user.Password);


            if (result_password == PasswordVerificationResult.Failed)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Неверный пароль"

                };
            }

            return new ServiceResult
            {
                IsSuccess = true
            };
            
            

        }

        //public async Task<string> GenerateCode()
        //{

        //}
    }
}
