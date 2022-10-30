namespace OurTube.API.Schemas.Types
{
    public class QueueType
    {
        public String Id { get; set; }
        public int Offset { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<VideoType> Videos { get; set; }
    }
}
