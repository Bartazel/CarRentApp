using CarRentApp.Api.Models;
using CarRentApp.Commands;
using CarRentApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace CarRentApp.Api.Controllers
{
    [ApiController]
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a car", OperationId = nameof(Car))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Car was successfully added")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Location was not found", typeof(ProblemDetails), "application/problem+json")]
        public async Task<IActionResult> Car(
            [FromBody, SwaggerRequestBody("Car request payload", Required = true)] CarRequest model)
        {
            var command = new AddCar(
                model.Model,
                model.Brand, 
                model.PricePerHour,
                model.LocationId);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all cars that are matching criteria", OperationId = nameof(GetCars))]
        [SwaggerResponse(StatusCodes.Status200OK, "Requested cars", typeof(GetCarsResponse), "application/json")]
        public async Task<IActionResult> GetCars(
            [FromQuery] DateTime? from, DateTime? to, string? locationId)
        {
            var query = new GetCars(from, to, locationId);
            var cars = await _mediator.Send(query);

            var response = cars?.Select(c => new GetCarsResponse(
                c.Id,
                c.Model,
                c.Brand,
                c.PricePerHour));

            return Ok(response);
        }
    }
}
