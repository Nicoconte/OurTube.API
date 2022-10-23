using Microsoft.EntityFrameworkCore;
using OurTube.API.Entities;

namespace OurTube.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomConfiguration> RoomsConfiguration { get; set; }
    }
}
