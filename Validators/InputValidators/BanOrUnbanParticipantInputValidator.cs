using FluentValidation;
using OurTube.API.Entities;
using OurTube.API.Schemas.Inputs;
using OurTube.API.Schemas.Types;
using OurTube.API.UseCases.Rooms.Commands;

namespace OurTube.API.Validators.InputValidators
{
    public class BanOrUnbanParticipantInputValidator : AbstractValidator<BanOrUnbanParticipantInputType>
    {

        public BanOrUnbanParticipantInputValidator()
        {
            RuleFor(c => c.RoomId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Room required.");

            RuleFor(c => c.ParticipantUsername)
                .NotNull()
                .NotEmpty()
                .WithMessage("Participant username cannot be empty. Its required");
        }
    }
}
