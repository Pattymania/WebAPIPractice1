using FluentValidation;
using WebAPIPractice.Models.Domain.DTO;

namespace WebAPIPractice.Validator
{
    public class UpdateWalkValidator:AbstractValidator<UpdateWalkRequest>
    {
        public UpdateWalkValidator()
        {
            RuleFor(i => i.Name).NotEmpty();
            RuleFor(i => i.Length).NotEmpty().GreaterThan(0);
            RuleFor(i => i.WalkDifficultyId).NotEmpty();
        }
    }
}
