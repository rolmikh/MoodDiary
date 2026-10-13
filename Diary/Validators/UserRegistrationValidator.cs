using Diary.DTO;
using FluentValidation;

namespace Diary.Validators
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidator() 
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage("Поле электронной почты должно быть заполнено")
                .EmailAddress()
                    .WithMessage("Поле должно соответствовать формату электронной почты")
                .MaximumLength(254)
                    .WithMessage("Электронная почта не должна превышать 254 символа");

            RuleFor(user => user.Password)
                .NotEmpty()
                    .WithMessage("Поле пароля должно быть заполнено")
                .MinimumLength(10)
                    .WithMessage("Пароль должен быть не менее 10 символов")
                .MaximumLength(128)
                    .WithMessage("Пароль не должен превышать 128 символов");

        }
    }
}
