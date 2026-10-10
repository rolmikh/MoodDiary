using Diary.Models;
using FluentValidation;

namespace Diary.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(user => user.Email)
                .NotNull();

            RuleFor(user => user.Password)
                .NotNull();

        }
    }
}
