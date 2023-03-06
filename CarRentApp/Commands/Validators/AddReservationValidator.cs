using FluentValidation;

namespace CarRentApp.Commands.Validators
{
    public class AddReservationValidator : AbstractValidator<AddReservation>
    {
        public AddReservationValidator() 
        {
            RuleFor(model => model.CarIds).NotEmpty();
            RuleFor(model => model.From).NotEmpty();
            RuleFor(model => model.To).NotEmpty().GreaterThan(model => model.From);
            RuleFor(model => model.PickupLocationId).NotEmpty();
        }
    }
}
