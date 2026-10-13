using Diary.DTO;
using FluentValidation;

namespace Diary.Validators
{
    public class UserAuthorizationValidator : AbstractValidator<AuthorizationDTO>
    {
        public UserAuthorizationValidator() 
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotEmpty();

        }

    }
}
