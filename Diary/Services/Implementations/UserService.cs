using Diary.DTO;
using Diary.Models;
using Diary.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Diary.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MoodDiaryDBContext _dbContext;
        private readonly IValidator<UserRegistrationDTO> _registration_validator;
        private readonly IValidator<AuthorizationDTO> _authorization_validator;

        private readonly PasswordHasher<User> _passwordHasher;

        private readonly IEmailService _emailService;

        
        public UserService(MoodDiaryDBContext context, IValidator<UserRegistrationDTO> registration_validator, IValidator<AuthorizationDTO> authorization_validator, PasswordHasher<User> passwordHasher, IEmailService emailService) 
        {
            _dbContext = context;
            _registration_validator = registration_validator;
            _authorization_validator = authorization_validator;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        
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

            await SendConfirmationCode(newUser.Email);

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

            if (foundUser.EmailConfirmed == false)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Электронная почта не подтверждена"

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

        public async Task<ServiceResult> SendConfirmationCode(string email)
        {

            string code = GenerateCode();

            string body = code;

            string subject = "Код подтверждения";

            try
            {
                if (await SaveCodeToDatabase(code, email) == false)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Error = "Пользователя не существует"

                    };
                }

                if (await _emailService.SendEmailMessage(email, body, subject) == false)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Error = "Не получилось отправить письмо"

                    };
                }

                return new ServiceResult
                {
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Ошибка"

                };
            }

        }

        public async Task<bool> SaveCodeToDatabase(string code, string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                {
                    return false;
                }

                UserVerificationToken userVerificationToken = new UserVerificationToken(user.IdUser, code, TypeOperationEnum.EmailConfirmation);

                _dbContext.UserVerificationTokens.Add(userVerificationToken);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception)
            {
                return false; 
            }

        }

        public async Task<ServiceResult> ConfirmEmail(string code, string email)
        {
            try
            {
                var foundUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (foundUser == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Error = "Пользователь не найден"

                    };
                }

                var userToken = await _dbContext.UserVerificationTokens.FirstOrDefaultAsync(s => s.UserId == foundUser.IdUser && s.OperationType == TypeOperationEnum.EmailConfirmation);

                if (userToken == null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Error = "Ошибка"

                    };
                }

                if (userToken.UsedAt != null)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Error = "По данному коду уже подтверждали электронную почту"

                    };
                }

                if (DateTime.UtcNow > userToken.ExpirationDate)
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Error = "Срок действия кода истек"

                    };
                }

                

                if (code == userToken.HashData)
                {

                    foundUser.SetEmailConfirmed();

                    userToken.SetUsedAt();

                    await _dbContext.SaveChangesAsync();

                    return new ServiceResult
                    {
                        IsSuccess = true
                    };
                }

                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Электронная почта не подтверждена"

                };



            }
            catch (Exception)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Error = "Ошибка"

                };
            }
           
        }

        public string GenerateCode()
        {

            return RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        }
    }
}
