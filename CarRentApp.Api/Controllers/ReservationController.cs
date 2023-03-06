using CarRentApp.Api.Models;
using CarRentApp.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CarRentApp.Api.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Reservation(
            [FromBody, SwaggerRequestBody("Car request payload", Required = true)] ReservationRequest model)
        {
            var command = new AddReservation(
                model.CarIds,
                model.From, 
                model.To,
                model.PickupLocationId,
                model.ReturnLocationId);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
