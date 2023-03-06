using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CarRentApp.Api.Models
{
    public class LocationRequest
    {
        public LocationRequest(string city, string address) 
        {
            City = city;
            Address = address;
        }

        [Required]
        [SwaggerSchema(Description = "City of a rental office")]
        public string City { get; set; }
        [Required]
        [SwaggerSchema(Description = "Address of a rental office")]
        public string Address { get; set; }
    }
}
