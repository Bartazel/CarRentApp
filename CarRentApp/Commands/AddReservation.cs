using CarRentApp.Exceptions;
using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp.Commands
{
    public record AddReservation(
        List<string> CarIds, 
        DateTime From, 
        DateTime To,
        string PickupLocationId,
        string? ReturnLocationId) : IRequest;
    public class AddReservationHandler : IRequestHandler<AddReservation>
    {
        private readonly CarRentAppDbContext _dbContext;
        public AddReservationHandler(CarRentAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddReservation request, CancellationToken cancellationToken)
        {
            var timeSpan = request.To.Subtract(request.From);
            var cars = await _dbContext.Cars.Include(c => c.Reservations)
                .Where(c => request.CarIds.Contains(c.Id))
                .ToListAsync(cancellationToken);

            if (!cars.Any())
            {
                throw new CarNotFoundException("No car was found.");
            }

            decimal price = 0;

            foreach(var car in cars) 
            {
                if (car.Reservations != null 
                    && car.Reservations.Any(r => r.From <= request.To && request.From <= r.To))
                {
                    throw new AddingReservationException($"Car with id {car.Id} already has a reservation in a give date range.");
                }
                price += car.PricePerHour * ((decimal)timeSpan.TotalHours);
            }

            Location? pickupLocation = await _dbContext.Locations
                .FirstOrDefaultAsync(l => l.Id == request.PickupLocationId, cancellationToken);
            Location? returnLocation = await _dbContext.Locations
                .FirstOrDefaultAsync(l => l.Id == request.ReturnLocationId, cancellationToken);

            if (pickupLocation == null) 
            {
                throw new LocationNotFoundException("Pickup location was not found.");
            }

            Reservation reservation = new Reservation()
            {
                Id = Guid.NewGuid().ToString(),
                Cars = cars,
                From = request.From,
                To = request.To,
                Price = price,
                PickupLocation = pickupLocation,
                ReturnLocation = returnLocation,
            };

            await _dbContext.Reservations.AddAsync(reservation, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
