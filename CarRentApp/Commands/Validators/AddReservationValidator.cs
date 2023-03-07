using FluentValidation;

namespace CarRentApp.Commands.Validators
{
    public class AddReservationValidator : AbstractValidator<AddReservation>
    {
        public AddReservationValidator() 
        {
            RuleFor(model => model.CarIds).NotEmpty();
            RuleFor(model => model.From).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(model => model.To).NotEmpty().GreaterThan(model => model.From).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(model => model.PickupLocationId).NotEmpty();
        }
    }
}
