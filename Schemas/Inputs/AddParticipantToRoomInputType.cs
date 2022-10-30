namespace OurTube.API.Schemas.Inputs
{
    public class AddParticipantToRoomInputType
    {
        public String RoomId { get; set; }
        public String ParticipantUsername { get; set; }
        public String? RoomPassword { get; set; }
    }
}
