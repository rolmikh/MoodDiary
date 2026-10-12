using Diary.DTO;
using Diary.Models;
using FluentValidation;

namespace Diary.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidator() 
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty();

        }
    }
}
