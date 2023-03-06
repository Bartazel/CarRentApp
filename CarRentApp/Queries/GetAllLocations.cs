using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp.Queries
{
    public record GetAllLocations() : IRequest<List<Location>>;
    public class GetAllLocationsHandler : IRequestHandler<GetAllLocations, List<Location>>
    {
        private readonly CarRentAppDbContext _dbContext;
        public GetAllLocationsHandler(CarRentAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Location>> Handle(GetAllLocations request, CancellationToken cancellationToken)
        {
            List<Location> locations = await _dbContext.Locations.ToListAsync(cancellationToken);
            
            return locations;
        }
    }
}
