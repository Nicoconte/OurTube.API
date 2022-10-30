using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurTube.API.Entities
{
    public class Queue
    {
        [Key]
        public String Id { get; set; }
        public int Offset { get; set; }
        public virtual List<Video> Videos { get; set; }   
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
