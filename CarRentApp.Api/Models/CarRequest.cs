using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CarRentApp.Api.Models
{
    public class CarRequest
    {
        public CarRequest(
            string model,
            string brand,
            decimal pricePerHour,
            string? locationId) 
        {
            Model = model;
            Brand = brand;
            PricePerHour = pricePerHour;
            LocationId = locationId;
        }

        [Required]
        [SwaggerSchema(Description = "Model of a car")]
        public string Model { get; set; }
        [Required]
        [SwaggerSchema(Description = "Brand of a car")]
        public string Brand { get; set; }
        [Required]
        [SwaggerSchema(Description = "Price to be paid by customer for renting a car for an hour")]
        public decimal PricePerHour { get; set; }
        [SwaggerSchema(Description = "Id of a location car will be at")]
        public string? LocationId { get; set; }
    }
}
