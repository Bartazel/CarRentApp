namespace CarRentApp.Repository.Entities
{
    public class Location
    {
        public string Id { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public ICollection<Car>? Cars { get; set; }
    }
}
