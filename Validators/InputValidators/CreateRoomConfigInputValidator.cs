using FluentValidation;
using OurTube.API.Data;
using OurTube.API.Entities;
using OurTube.API.Schemas.Inputs;

namespace OurTube.API.Validators.InputValidators
{
    public class CreateRoomConfigInputValidator : AbstractValidator<CreateRoomConfigurationInputType>
    {
        public CreateRoomConfigInputValidator()
        {
            RuleFor(c => c.Password)
                .Must(p => !string.IsNullOrEmpty(p))
                .When(c => !c.IsPublic)
                .WithMessage("Room is private. A Password is required");

            RuleFor(c => c.MaxParticipants)
                .Must(p => p < 15)
                .When(c => c.MaxParticipants > 15)
                .WithMessage("Cannot more than 15 participants in a room");
        }
    }
}
