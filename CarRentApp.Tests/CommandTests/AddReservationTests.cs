using CarRentApp.Commands;
using CarRentApp.Exceptions;
using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentApp.Tests.CommandTests
{
    public class AddReservationTests
    {
        [Test]
        public void Should_throw_exception_when_car_already_has_reservation()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            var handler = new AddReservationHandler(context);

            var addReservation = new AddReservation(
                new List<string>() { "car_id" },
                new DateTime(),
                new DateTime(),
                "location_id",
                "location_id");

            Assert.ThrowsAsync(typeof(AddingReservationException), () => handler.Handle(addReservation, CancellationToken.None));
        }

        [Test]
        public void Should_throw_exception_when_pickupLocation_was_not_found()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            var handler = new AddReservationHandler(context);

            var addReservation = new AddReservation(
                new List<string>() { "car_id" },
                new DateTime(),
                new DateTime(),
                "wrong_location_id",
                "location_id");

            Assert.ThrowsAsync(typeof(AddingReservationException), () => handler.Handle(addReservation, CancellationToken.None));
        }

        [Test]
        public async Task Should_add_reservation()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);

            var handler = new AddReservationHandler(context);

            context.Cars.Add(new Car()
            {
                Id = "car_id",
                Brand = "brand",
                Model = "model"
            });
            context.Locations.Add(new Location()
            {
                Id = "location_id",
                City = "city",
                Address = "address"
            });

            await context.SaveChangesAsync();

            var addReservation = new AddReservation(
                new List<string>() { "car_id" },
                new DateTime(),
                new DateTime(),
                "location_id",
                "location_id");

            await handler.Handle(addReservation, CancellationToken.None);

            var reservation = context.Reservations.FirstOrDefault();
            Assert.IsNotNull(reservation);
        }
    }
}
