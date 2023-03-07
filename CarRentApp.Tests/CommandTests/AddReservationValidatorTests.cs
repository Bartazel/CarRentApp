using CarRentApp.Commands;
using CarRentApp.Commands.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentValidation.TestHelper;

namespace CarRentApp.Tests.CommandTests
{
    public class AddReservationValidatorTests
    {
        private DateTime _currentTime = DateTime.Now;
        private readonly AddReservationValidator _addReservationValidator;

        public AddReservationValidatorTests() 
        {
            _addReservationValidator = new AddReservationValidator();
        }

        [Test]
        public void Should_have_error_when_to_is_before_from()
        {
            var addReservation = CreateAddReservation(
                new List<string> { "car_id" },
                _currentTime.AddDays(1),
                _currentTime);

            var response = _addReservationValidator.TestValidate(addReservation);
            response.ShouldHaveValidationErrorFor(r => r.To);
        }

        [Test]
        public void Should_have_error_when_carIds_is_null()
        {
            var addReservation = CreateAddReservation(
                null,
                _currentTime,
                _currentTime.AddDays(1));

            var response = _addReservationValidator.TestValidate(addReservation);
            response.ShouldHaveValidationErrorFor(r => r.CarIds);
        }

        [Test]
        public void Should_not_have_error_when_to_is_after_from()
        {
            var addReservation = CreateAddReservation(
                new List<string> { "car_id" },
                _currentTime,
                _currentTime.AddDays(1));

            var response = _addReservationValidator.TestValidate(addReservation);
            response.ShouldNotHaveValidationErrorFor(r => r.To);
        }

        private AddReservation CreateAddReservation(
            List<string>? carIds, 
            DateTime from, 
            DateTime to)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return new AddReservation(
                carIds,
                from,
                to,
                "location_id",
                "location_id");
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
