using FluentValidation;
using WebAPIPractice.Models.Domain.DTO;

namespace WebAPIPractice.Validator
{
    public class UpdateRegionValidator:AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Code).NotEmpty();
            RuleFor(i => i.Area).GreaterThan(0);
            RuleFor(i => i.Population).GreaterThanOrEqualTo(0);
        }
    }
}
