using CarRentApp.Exceptions;
using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp.Commands
{
    public record AddCar(
        string Model, 
        string Brand,
        decimal PricePerHour,
        string? LocationId) : IRequest;
    public class AddCarHandler : IRequestHandler<AddCar>
    {
        private readonly CarRentAppDbContext _dbContext;
        public AddCarHandler(CarRentAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddCar request, CancellationToken cancellationToken)
        {
            Location? location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == request.LocationId, cancellationToken);

            if (location == null)
            {
                throw new LocationNotFoundException("Location was not found.");
            }

            Car car = new Car()
            {
                Id = Guid.NewGuid().ToString(),
                Model = request.Model,
                Brand = request.Brand,
                PricePerHour = request.PricePerHour,
                Location = location,
            };

            await _dbContext.Cars.AddAsync(car, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
