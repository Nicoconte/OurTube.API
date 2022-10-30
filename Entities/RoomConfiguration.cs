using System.ComponentModel.DataAnnotations;

namespace OurTube.API.Entities
{
    public class RoomConfiguration
    {
        [Key]
        public String Id { get; set; }
        public bool IsPublic { get; set; }
        public String Password { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
