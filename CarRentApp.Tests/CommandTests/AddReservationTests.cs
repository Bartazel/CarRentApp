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
        private DateTime _currentTime = DateTime.Now;

        [Test]
        public async Task Should_throw_exception_when_car_was_not_found()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);

            context.Locations.Add(new Location()
            {
                Id = "location_id",
                City = "city",
                Address = "address"
            });
            await context.SaveChangesAsync();

            var handler = new AddReservationHandler(context);
            var addReservation = CreateAddReservation("location_id", "location_id");

            var ex = Assert.ThrowsAsync(typeof(CarNotFoundException), () => handler.Handle(addReservation, CancellationToken.None));
            Assert.That(ex?.Message, Is.EqualTo("No car was found."));
        }

        [Test]
        public void Should_throw_exception_when_car_already_has_reservation()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);
            TestDbHelper.PopulateData(context);

            var handler = new AddReservationHandler(context);
            var addReservation = CreateAddReservation("location_id", "location_id");
            var carId = context.Cars.First().Id;

            var ex = Assert.ThrowsAsync(typeof(AddingReservationException), () => handler.Handle(addReservation, CancellationToken.None));
            Assert.That(ex?.Message, Is.EqualTo($"Car with id {carId} already has a reservation in a give date range."));
        }

        [Test]
        public async Task Should_throw_exception_when_pickupLocation_was_not_found()
        {
            using var context = new CarRentAppDbContext(TestDbHelper.Options);

            TestDbHelper.Cleanup(context);

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

            var handler = new AddReservationHandler(context);

            var addReservation = CreateAddReservation("wrong_location_id", "location_id");

            var ex = Assert.ThrowsAsync(typeof(LocationNotFoundException), () => handler.Handle(addReservation, CancellationToken.None));
            Assert.That(ex?.Message, Is.EqualTo("Pickup location was not found."));
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

            var addReservation = CreateAddReservation(
                "location_id",
                "location_id");

            await handler.Handle(addReservation, CancellationToken.None);

            var reservation = context.Reservations.FirstOrDefault();
            Assert.That(
                reservation != null &&
                reservation.Cars.First().Id == "car_id" &&
                reservation.From.Equals(_currentTime) &&
                reservation.To.Equals(_currentTime.AddDays(1)) &&
                reservation.PickupLocation.Id == "location_id" &&
                reservation.ReturnLocation?.Id == "location_id");
        }

        private AddReservation CreateAddReservation(string pickupLocationId, string returnLocationId)
        {
            return new AddReservation(
                new List<string>() { "car_id" },
                _currentTime,
                _currentTime.AddDays(1),
                pickupLocationId,
                returnLocationId);
        }
    }
}
