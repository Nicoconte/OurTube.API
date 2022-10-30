using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurTube.API.Entities
{
    public class Participant
    {
        [Key]
        public String Id { get; set; }
        public String UserId { get; set; }
        public String RoomId { get; set; }
        public virtual User User { get; set; }
        public bool IsBanned { get; set; }
    }
}
