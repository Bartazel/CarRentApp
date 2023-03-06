using CarRentApp.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp.Repository
{
    public class CarRentAppDbContext : DbContext
    {
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

        public CarRentAppDbContext(DbContextOptions<CarRentAppDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
