namespace OurTube.API.Entities
{
    public class Queue
    {
        public String Id { get; set; }
        public int Offset { get; set; }
        public virtual List<Video> Videos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
