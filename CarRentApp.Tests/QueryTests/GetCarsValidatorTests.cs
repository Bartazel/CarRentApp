using CarRentApp.Queries;
using CarRentApp.Queries.Validators;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;

namespace CarRentApp.Tests.QueryTests
{
    public class GetCarsValidatorTests
    {
        private readonly GetCarsValidator _getCarsValidator;

        public GetCarsValidatorTests()
        {
            _getCarsValidator = new GetCarsValidator();
        }

        [Test]
        public void Should_have_error_when_to_is_smaller_than_from()
        {
            var getCars = new GetCars(
                new DateTime().AddDays(1),
                new DateTime(),
                "location_id");

            var response = _getCarsValidator.TestValidate(getCars);
            response.ShouldHaveValidationErrorFor(r => r.To);
        }

        [Test]
        public void Should_not_have_error_when_to_is_bigger_than_from()
        {
            var getCars = new GetCars(
                new DateTime(),
                new DateTime().AddDays(1),
                "location_id");

            var response = _getCarsValidator.TestValidate(getCars);
            response.ShouldNotHaveValidationErrorFor(r => r.To);
        }
    }
}
