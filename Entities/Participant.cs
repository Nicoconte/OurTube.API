namespace OurTube.API.Entities
{
    public class Participant
    {
        public String Id { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
