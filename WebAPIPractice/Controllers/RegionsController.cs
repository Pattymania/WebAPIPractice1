using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Repositories;

namespace WebAPIPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();
            var regionDTO = mapper.Map<List<Models.Domain.DTO.RegionDTO>>(regions);

            return Ok(regionDTO);
        }
    }
}
