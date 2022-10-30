using OurTube.API.UseCases.Rooms.Commands;

namespace OurTube.API.Schemas.Inputs
{
    public class BanOrUnbanParticipantInputType
    {
        public ParticipantOperationTypes Operation { get; set; }
        public String ParticipantUsername { get; set; }
        public String RoomId { get; set; }
    }
}
