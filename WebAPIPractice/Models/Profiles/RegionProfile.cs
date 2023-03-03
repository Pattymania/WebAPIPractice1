using AutoMapper;

namespace WebAPIPractice.Models.Profiles
{
    public class RegionProfile:Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.Domain.DTO.RegionDTO>();
            
        }
    }
}
