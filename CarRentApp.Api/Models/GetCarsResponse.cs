using Swashbuckle.AspNetCore.Annotations;

namespace CarRentApp.Api.Models
{
    public class GetCarsResponse
    {
        public GetCarsResponse(
            string id,
            string model,
            string brand,
            decimal pricePerHour)
        {
            Id = id;
            Model = model;
            Brand = brand;
            PricePerHour = pricePerHour;
        }

        [SwaggerSchema(Description = "Unique identifier", Nullable = false)]
        public string Id { get; set; }
        [SwaggerSchema(Description = "Model of a car", Nullable = false)]
        public string Model { get; set; }
        [SwaggerSchema(Description = "Brand of a car", Nullable = false)]
        public string Brand { get; set; }
        [SwaggerSchema(Description = "Price to be paid by customer for renting a car for an hour", Nullable = false)]
        public decimal PricePerHour { get; set; }
    }
}
