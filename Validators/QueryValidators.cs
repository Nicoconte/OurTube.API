using OurTube.API.Validators.InputValidators;

namespace OurTube.API.Validators
{
    public class QueryValidators
    {
        public RoomByIdInputTypeValidator RoomByIdInputTypeValidator { get; set; }

        public QueryValidators(RoomByIdInputTypeValidator roomByIdInputTypeValidator)
        {
            RoomByIdInputTypeValidator = roomByIdInputTypeValidator;
        }
    }
}
