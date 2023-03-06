namespace CarRentApp.Repository.Entities
{
    public class Reservation
    {
        public string Id { get; set; } = null!;
        public virtual ICollection<Car> Cars { get; set; } = null!;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Price { get; set; }
        public Location PickupLocation { get; set; } = null!;
        public Location? ReturnLocation { get; set; }
    }
}
