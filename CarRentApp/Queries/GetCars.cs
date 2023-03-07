using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp.Queries
{
    public record GetCars(DateTime? From, DateTime? To, string? LocationId) : IRequest<List<Car>>;
    public class GetCarsHandler : IRequestHandler<GetCars, List<Car>>
    {
        private readonly CarRentAppDbContext _dbContext;
        public GetCarsHandler(CarRentAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Car>> Handle(GetCars request, CancellationToken cancellationToken)
        {
            List<Car> cars = await _dbContext.Cars
                .Where(c => ((request.From == null && request.To == null) ||
                !c.Reservations!.Any(r => request.From <= r.To && r.From <= request.To))
                && (request.LocationId == null || c.Location.Id == request.LocationId))
                .ToListAsync(cancellationToken);

            return cars;
        }
    }
}
