using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using MediatR;

namespace CarRentApp.Commands
{
    public record AddLocation(string City, string Address) : IRequest;
    public class AddLocationHandler : IRequestHandler<AddLocation>
    {
        private readonly CarRentAppDbContext _dbContext;
        public AddLocationHandler(CarRentAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddLocation request, CancellationToken cancellationToken)
        {
            Location location = new Location()
            {
                Id = Guid.NewGuid().ToString(),
                City = request.City,
                Address = request.Address,
            };

            await _dbContext.Locations.AddAsync(location, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
