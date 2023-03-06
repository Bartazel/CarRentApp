using FluentValidation;

namespace CarRentApp.Queries.Validators
{
    public class GetCarsValidator : AbstractValidator<GetCars>
    {
        public GetCarsValidator() 
        {
            RuleFor(model => model.To).NotEmpty().GreaterThan(model => model.From).When(model => model.From != null);
        }
    }
}
