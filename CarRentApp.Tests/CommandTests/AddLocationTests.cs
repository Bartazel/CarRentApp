using CarRentApp.Commands;
using CarRentApp.Repository;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentApp.Tests.CommandTests
{
    public class AddLocationTests
    {
        [Test]
        public async Task Should_add_location()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);

            var handler = new AddLocationHandler(context);

            var addLocation = new AddLocation("city", "address");

            await handler.Handle(addLocation, CancellationToken.None);

            var location = context.Locations.FirstOrDefault();
            Assert.That(
                location != null &&
                location.City == "city" &&
                location.Address == "address");
        }
    }
}
