using Swashbuckle.AspNetCore.Annotations;

namespace CarRentApp.Api.Models
{
    public class GetAllLocationsResponse
    {
        public GetAllLocationsResponse(
            string id,
            string city,
            string address)
        {
            Id = id;
            City = city;
            Address = address;
        }

        [SwaggerSchema(Description = "Unique identifier", Nullable = false)]
        public string Id { get; set; }
        [SwaggerSchema(Description = "City of a rental office", Nullable = false)]
        public string City { get; set; }
        [SwaggerSchema(Description = "Address of a rental office", Nullable = false)]
        public string Address { get; set; }
    }
}
