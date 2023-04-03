using FluentValidation;
using WebAPIPractice.Models.Domain.DTO;

namespace WebAPIPractice.Validator
{
    public class AddWalkValidator:AbstractValidator<AddWalkRequest>
    {
        public AddWalkValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i=> i.Length).NotEmpty().GreaterThan(0);
            RuleFor(i => i.WalkDifficultyId).NotEmpty();
            RuleFor(i=>i.RegionID).NotEmpty();
        }
    }
}
