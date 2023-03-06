using CarRentApp.Queries;
using CarRentApp.Repository;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentApp.Tests.QueryTests
{
    public class GetCarsTests
    {
        [Test]
        public async Task Should_return_cars()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);
            
            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            await context.SaveChangesAsync();

            var handler = new GetCarsHandler(context);

            var getCars = new GetCars(
                new DateTime(),
                new DateTime(),
                "location_id");

            var cars = await handler.Handle(getCars, CancellationToken.None);
            Assert.AreEqual(1, cars.Count);
        }
    }
}
