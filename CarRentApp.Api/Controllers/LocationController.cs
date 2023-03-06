using CarRentApp.Api.Models;
using CarRentApp.Commands;
using CarRentApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRentApp.Api.Controllers
{
    [ApiController]
    [Route("api/location")]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Location(
            [FromBody, SwaggerRequestBody("Location request payload", Required = true)] LocationRequest model)
        {
            var command = new AddLocation(model.City, model.Address);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("all-locations")]
        [SwaggerOperation(Summary = "Get all locations", OperationId = nameof(GetAllLocations))]
        [SwaggerResponse(StatusCodes.Status200OK, "All locations", typeof(GetAllLocationsResponse), "application/json")]
        public async Task<IActionResult> GetAllLocations()
        {
            var query = new GetAllLocations();
            var cars = await _mediator.Send(query);

            var response = cars?.Select(c => new GetAllLocationsResponse(
                c.Id,
                c.City,
                c.Address));

            return Ok(response);
        }
    }
}
