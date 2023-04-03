using FluentValidation;
using WebAPIPractice.Models.Domain.DTO;

namespace WebAPIPractice.Validator
{
    public class UpdateWalkDifficultyValidator:AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyValidator()
        {
            RuleFor(i => i.Code).NotEmpty();
        }
    }
}
