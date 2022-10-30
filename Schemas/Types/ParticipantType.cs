namespace OurTube.API.Schemas.Types
{
    public class ParticipantType
    {
        public String Id { get; set; }
        public UserType User { get; set; }
        public bool IsBanned { get; set; }
        public String RoomId { get; set; }
    }
}
