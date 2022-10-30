namespace OurTube.API.Schemas.Inputs
{
    public class CreateRoomConfigurationInputType
    {
        public bool IsPublic { get; set; }
        public String Password { get; set; }
        public int MaxParticipants { get; set; }

    }
}
