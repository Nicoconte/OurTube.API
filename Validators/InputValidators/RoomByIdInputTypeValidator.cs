using FluentValidation;
using OurTube.API.Schemas.Inputs;

namespace OurTube.API.Validators.InputValidators
{
    public class RoomByIdInputTypeValidator : AbstractValidator<RoomByIdInputType>
    {
        public RoomByIdInputTypeValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty().NotNull().WithMessage("Room ID is required");
        }
    }
}
