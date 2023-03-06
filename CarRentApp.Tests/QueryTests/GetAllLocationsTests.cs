using CarRentApp.Queries;
using CarRentApp.Repository;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentApp.Tests.QueryTests
{
    public class GetAllLocationsTests
    {
        [Test]
        public async Task Should_return_all_locations()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            await context.SaveChangesAsync();

            var handler = new GetAllLocationsHandler(context);

            var getAllLocations = new GetAllLocations();

            var locations = await handler.Handle(getAllLocations, CancellationToken.None);
            Assert.AreEqual(1, locations.Count);
        }
    }
}
