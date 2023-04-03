using FluentValidation;
using WebAPIPractice.Models.Domain.DTO;

namespace WebAPIPractice.Validator
{
    public class AddWalkDifficultyValidator:AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyValidator()
        {
            RuleFor(i => i.Code).NotEmpty();
        }
    }
}
