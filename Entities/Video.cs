using System.ComponentModel.DataAnnotations;

namespace OurTube.API.Entities
{
    public class Video
    {
        [Key]
        public String Id { get; set; }
        public String QueueId { get; set; }
        public String SourceUrl { get; set; }
    }
}
