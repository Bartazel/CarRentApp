using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CarRentApp.Api.Models
{
    [SwaggerSchema]
    public class ReservationRequest
    {
        public ReservationRequest(
            List<string> carIds,
            DateTime from,
            DateTime to,
            string pickupLocationId,
            string? returnLocationId)
        {
            CarIds = carIds;
            From = from;
            To = to;
            PickupLocationId = pickupLocationId;
            ReturnLocationId = returnLocationId;
        }

        [Required]
        [SwaggerSchema(Description = "Ids of cars to be rented")]
        public List<string> CarIds { get; set; }
        [Required]
        [SwaggerSchema(Description = "Date from which the car will be rented out")]
        public DateTime From { get; set; }
        [Required]
        [SwaggerSchema(Description = "Date from which the car reservation will expire")]
        public DateTime To { get; set; }
        [Required]
        [SwaggerSchema(Description = "Location from which the car reservation will be picked up")]
        public string PickupLocationId { get; set; }
        [SwaggerSchema(Description = "Location at which the car will be returned")]
        public string? ReturnLocationId { get; set; }
    }
}
