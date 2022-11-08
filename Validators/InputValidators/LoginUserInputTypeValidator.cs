using FluentValidation;
using OurTube.API.Schemas.Inputs;

namespace OurTube.API.Validators.InputValidators
{
    public class LoginUserInputTypeValidator : AbstractValidator<LoginUserInputType>
    {
        public LoginUserInputTypeValidator()
        {
            RuleFor(s => s.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required");

            RuleFor(s => s.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password is required");
        }
    }
}
