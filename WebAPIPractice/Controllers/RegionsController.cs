using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using WebAPIPractice.Command;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Models.Domain.DTO;
using WebAPIPractice.Queries;
using WebAPIPractice.Repositories;

namespace WebAPIPractice.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    //[Authorize]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private IMapper mapper;

        public IMediator MediatR { get; }

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, IMediator mediatR)
        {
            this._regionRepository = regionRepository;
            this.mapper = mapper;
            MediatR = mediatR;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //var regions = await _regionRepository.GetAllAsync();
            //var regionDTO = mapper.Map<List<Models.Domain.DTO.RegionDTO>>(regions);

            var regions = await MediatR.Send(new GetRegionListQuery());
            var regionDTO = mapper.Map<List<Models.Domain.DTO.RegionDTO>>(regions);

            return Ok(regionDTO);
        }

        [HttpGet]
        //[Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync([FromQuery] Guid Id)
        {
            //var region = await _regionRepository.GetAsync(Id);
            //if (region == null)
            //{
            //    return NotFound();
            //}

            var query = new GetRegionQuery(Id);
            var region = await MediatR.Send(query);

            var regionDTO = mapper.Map<Models.Domain.DTO.RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync( Models.Domain.DTO.AddRegionRequest addRegionRequest)
        {
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };

            //region = await _regionRepository.AddAsync(region);

            var model = new AddRegionCommand(region);
            await MediatR.Send(model);

            var regionDTO = new Models.Domain.DTO.RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = region.Id }, regionDTO);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.Domain.DTO.RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, UpdateRegionRequest updateRegionRequest)
        {
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,
            };

            var updateRegion = await _regionRepository.UpdateAsync(id, region);
            if(updateRegion == null)
            {
                return NotFound();
            }
            var RegionDTO = mapper.Map<Models.Domain.DTO.RegionDTO>(updateRegion);

            return Ok(RegionDTO);

        } 
    }
}
