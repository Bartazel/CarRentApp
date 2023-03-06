using CarRentApp.Commands;
using CarRentApp.Repository.Entities;
using CarRentApp.Repository;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarRentApp.Exceptions;

namespace CarRentApp.Tests.CommandTests
{
    public class AddCarTests
    {
        [Test]
        public async Task Should_add_car()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);

            var handler = new AddCarHandler(context);

            context.Locations.Add(new Location()
            {
                Id = "location_id",
                City = "city",
                Address = "address"
            });

            await context.SaveChangesAsync();

            var addCar = new AddCar(
                "model",
                "brand",
                10,
                "location_id");

            await handler.Handle(addCar, CancellationToken.None);

            var car = context.Cars.FirstOrDefault();
            Assert.IsNotNull(car);
        }

        [Test]
        public void Should_throw_exception_when_location_was_not_found()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            var handler = new AddCarHandler(context);

            var addCar = new AddCar(
                "model",
                "brand",
                10,
                "wrong_location_id");

            Assert.ThrowsAsync(typeof(AddingCarException), () => handler.Handle(addCar, CancellationToken.None));
        }
    }
}
