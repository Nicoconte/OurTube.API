using FluentValidation;
using OurTube.API.Schemas.Inputs;

namespace OurTube.API.Validators.InputValidators
{
    public class CreateRoomInputValidator : AbstractValidator<CreateRoomInputType>
    {
        public CreateRoomInputValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Room name cannot be empty. Its required");
        }
    }
}
