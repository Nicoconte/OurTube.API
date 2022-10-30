 namespace OurTube.API.Schemas.Types
{
    public class RoomType
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String InviteCode { get; set; }
        public UserType Owner { get; set; }
        public List<ParticipantType> Participants { get; set; }
        public RoomConfigurationType Configuration { get; set; }
        public QueueType Queue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
