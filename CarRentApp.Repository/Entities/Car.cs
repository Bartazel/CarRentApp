namespace CarRentApp.Repository.Entities
{
    public class Car
    {
        public string Id { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!; 
        public decimal PricePerHour { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; }
        public Location Location { get; set; } = null!;
    }
}
