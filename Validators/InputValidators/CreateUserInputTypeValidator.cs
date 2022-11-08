using FluentValidation;
using OurTube.API.Schemas.Inputs;

namespace OurTube.API.Validators.InputValidators
{
    public class CreateUserInputTypeValidator : AbstractValidator<CreateUserInputType>
    {
        public CreateUserInputTypeValidator()
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
