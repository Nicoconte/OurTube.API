using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurTube.API.Entities
{
    public class Room
    {
        [Key]
        public String Id { get; set; }
        public String Name { get; set; }
        public String InviteCode { get; set; }
        public String OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual List<Participant> Participants { get; set; }
        public virtual RoomConfiguration Configuration { get; set; }
        public virtual Queue Queue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
