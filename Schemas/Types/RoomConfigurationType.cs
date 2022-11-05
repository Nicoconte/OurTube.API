namespace OurTube.API.Schemas.Types
{
    public class RoomConfigurationType
    {
        public String Id { get; set; }
        public bool IsPublic { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [GraphQLIgnore]
        public String Password { get; set; }
    }
}
