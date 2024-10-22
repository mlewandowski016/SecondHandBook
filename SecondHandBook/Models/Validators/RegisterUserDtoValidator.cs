using FluentValidation;
using SecondHandBook.Entities;

namespace SecondHandBook.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(SecondHandBookDbContext dbContext)
        {
            RuleFor(x => x.Password).MinimumLength(5);

            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password);

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var usedEmail = dbContext.Users.Any(u => u.Email == value);
                if (usedEmail) context.AddFailure("Email", "This email is in use");
            });
        }
    }
}
