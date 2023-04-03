using FluentValidation;
using WebAPIPractice.Models.Domain.DTO;

namespace WebAPIPractice.Validator
{
    public class AddRegionValidator : AbstractValidator<AddRegionRequest>
    {
        public AddRegionValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(i => i.Code).NotEmpty().WithMessage("Code is Required");
            RuleFor(i => i.Area).GreaterThan(0).WithMessage("Area should be greater than 0");
            RuleFor(i => i.Population).GreaterThanOrEqualTo(0).WithMessage("Population cannot be empty or negative");
        }
    }
}
