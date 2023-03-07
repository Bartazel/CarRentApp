using CarRentApp.Api.Models;
using CarRentApp.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static System.Net.Mime.MediaTypeNames;

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
        [SwaggerOperation(Summary = "Add a reservation", OperationId = nameof(Reservation))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Reservation was successfully added")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No car was found", typeof(ProblemDetails), "application/problem+json")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Pickup location was not found", typeof(ProblemDetails), "application/problem+json")]
        public async Task<IActionResult> Reservation(
            [FromBody, SwaggerRequestBody("Reservation request payload", Required = true)] ReservationRequest model)
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
