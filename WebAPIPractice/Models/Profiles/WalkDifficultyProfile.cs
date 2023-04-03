using AutoMapper;

namespace WebAPIPractice.Models.Profiles
{
    public class WalkDifficultyProfile : Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.Domain.DTO.WalksDifficultyDTO>();
        }
    }
}
