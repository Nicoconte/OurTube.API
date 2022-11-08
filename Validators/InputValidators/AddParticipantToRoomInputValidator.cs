using FluentValidation;
using OurTube.API.Schemas.Inputs;

namespace OurTube.API.Validators.InputValidators
{
    public class AddParticipantToRoomInputValidator : AbstractValidator<AddParticipantToRoomInputType>
    {
        public AddParticipantToRoomInputValidator()
        {
            RuleFor(c => c.RoomId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Room required.");

            RuleFor(c => c.ParticipantUsername)
                .NotNull()
                .NotEmpty()
                .WithMessage("Participant username cannot be empty. Its required.");
        }
    }
}
