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
        private readonly AddReservationValidator _addReservationValidator;

        public AddReservationValidatorTests() 
        {
            _addReservationValidator = new AddReservationValidator();
        }

        [Test]
        public void Should_have_error_when_to_is_smaller_than_from()
        {
            var addReservation = new AddReservation(
                new List<string> { "1234" },
                new DateTime().AddDays(1),
                new DateTime(),
                "1234",
                "1234");

            var response = _addReservationValidator.TestValidate(addReservation);
            response.ShouldHaveValidationErrorFor(r => r.To);
        }

        [Test]
        public void Should_have_error_when_carIds_is_null()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var addReservation = new AddReservation(
                null,
                new DateTime().AddDays(1),
                new DateTime(),
                "1234",
                "1234");
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            var response = _addReservationValidator.TestValidate(addReservation);
            response.ShouldHaveValidationErrorFor(r => r.CarIds);
        }

        [Test]
        public void Should_not_have_error_when_to_is_bigger_than_from()
        {
            var addReservation = new AddReservation(
                new List<string> { "1234" },
                new DateTime(),
                new DateTime().AddDays(1),
                "1234",
                "1234");

            var response = _addReservationValidator.TestValidate(addReservation);
            response.ShouldNotHaveValidationErrorFor(r => r.To);
        }
    }
}
