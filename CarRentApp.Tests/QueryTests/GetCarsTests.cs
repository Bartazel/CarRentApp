using CarRentApp.Queries;
using CarRentApp.Repository;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentApp.Tests.QueryTests
{
    public class GetCarsTests
    {
        private DateTime _currentTime = DateTime.Now;

        [Test]
        public async Task Should_return_cars()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);
            
            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            await context.SaveChangesAsync();

            var handler = new GetCarsHandler(context);

            var getCars = new GetCars(
                _currentTime.AddDays(2),
                _currentTime.AddDays(3),
                "location_id");

            var cars = await handler.Handle(getCars, CancellationToken.None);
            Assert.AreEqual(1, cars.Count);
        }
    }
}
