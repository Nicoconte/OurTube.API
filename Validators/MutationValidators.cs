using OurTube.API.Schemas.Inputs;
using OurTube.API.Validators.InputValidators;

namespace OurTube.API.Validators
{
    public class MutationValidators
    {
        public CreateRoomInputValidator CreateRoomInputValidator { get; set; }
        public CreateRoomConfigInputValidator CreateRoomConfigInputValidator { get; set; }
        public BanOrUnbanParticipantInputValidator BanOrUnbanParticipantInputValidator { get; set; }
        public AddParticipantToRoomInputValidator AddParticipantToRoomInputValidator { get; set; }
        public CreateUserInputTypeValidator CreateUserInputTypeValidator { get; set; }
        public LoginUserInputTypeValidator LoginUserInputTypeValidator { get; set; }

        public MutationValidators(
            BanOrUnbanParticipantInputValidator banOrUnbanParticipantInputValidator,
            CreateRoomInputValidator createRoomInputValidator, 
            CreateRoomConfigInputValidator createRoomConfigInputValidator,
            AddParticipantToRoomInputValidator addParticipantToRoomInputValidator,
            CreateUserInputTypeValidator createUserInputTypeValidator,
            LoginUserInputTypeValidator loginUserInputTypeValidator)
        {
            CreateRoomInputValidator = createRoomInputValidator;
            CreateRoomConfigInputValidator = createRoomConfigInputValidator;
            BanOrUnbanParticipantInputValidator = banOrUnbanParticipantInputValidator;
            AddParticipantToRoomInputValidator = addParticipantToRoomInputValidator;
            CreateUserInputTypeValidator = createUserInputTypeValidator;
            LoginUserInputTypeValidator = loginUserInputTypeValidator;
        }
    }
}
