using AutoMapper;

namespace WebAPIPractice.Models.Profiles
{
    public class WalksProfile:Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Domain.Walk, Models.Domain.DTO.Walk>();
        }
    }
}
