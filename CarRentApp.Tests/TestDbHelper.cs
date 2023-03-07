using CarRentApp.Repository;
using CarRentApp.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CarRentApp.Tests
{
    public static class TestDbHelper
    {
        public static readonly DbContextOptions<CarRentAppDbContext> Options = new DbContextOptionsBuilder<CarRentAppDbContext>()
            .UseInMemoryDatabase(databaseName: "CarRentAppDatabase").Options;

        public static void PopulateData(CarRentAppDbContext context)
        {
            var location = new Location()
            {
                Id = "location_id",
                City = "city",
                Address = "address"
            };

            var car = new Car()
            {
                Id = "car_id",
                Model = "model",
                Brand = "brand",
                PricePerHour = 10,
                Location = location
            };

            var reservation = new Reservation()
            {
                Id = "reservation_id",
                Cars = new List<Car> { car },
                From = DateTime.Now,
                To = DateTime.Now.AddDays(1),
                Price = 10,
                PickupLocation = location,
                ReturnLocation = location
            };
            
            context.Locations.Add(location);
            context.Cars.Add(car);
            context.Reservations.Add(reservation);
            context.SaveChanges();
        }

        public static void Cleanup(CarRentAppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
