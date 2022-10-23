namespace OurTube.API.Entities
{
    public class User
    {
        public String Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
