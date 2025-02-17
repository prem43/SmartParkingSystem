using Microsoft.EntityFrameworkCore;

namespace SmartParkingSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet for Parking Lots
        public DbSet<ParkingLotsDTO> ParkingLots { get; set; }
    }
}
