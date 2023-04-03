using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Models.Domain.DTO;
using WebAPIPractice.Repositories;

namespace WebAPIPractice.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this._walkRepository = walkRepository;
            this._mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            var walk = await _walkRepository.GetAllAsync();
            var walkDTO = _mapper.Map<List<Models.Domain.DTO.Walk>>(walk);
            return Ok(walkDTO);
        }

        [HttpGet]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync([FromQuery] Guid Id)
        {
            var walk = await _walkRepository.GetAsync(Id);
            if(walk == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<Models.Domain.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            // Validation

            if(!(await AddWalkValidation(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionID = addWalkRequest.RegionID,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };
            var newWalk = await _walkRepository.AddAsync(walk);
            var walkDTO = new Models.Domain.DTO.Walk()
            {
                Id = newWalk.Id,
                Name = newWalk.Name,
                Length = newWalk.Length,
                RegionID = newWalk.RegionID,
                WalkDifficultyId = newWalk.WalkDifficultyId,
            };
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWalkAsync([FromQuery] Guid Id, UpdateWalkRequest updateWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionID = updateWalkRequest.RegionID,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            var updatedWalk = await _walkRepository.UpdateAsync(Id, walk);
            if(updatedWalk == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<Models.Domain.DTO.Walk>(updatedWalk);
            return Ok(walkDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWalkAsync([FromQuery] Guid Id)
        {
            var walkRecord = await _walkRepository.DeleteAsync(Id);
            if(walkRecord == null)
            {
                return NotFound();
            }
            var walkDTO = _mapper.Map<Models.Domain.DTO.Walk>(walkRecord);
            return Ok(walkDTO);
        }
        #region
        private async Task<bool> AddWalkValidation(AddWalkRequest addWalkRequest)
        {
            if(addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),$"{nameof(addWalkRequest)} cannot be empty.");
            }

            var region = regionRepository.GetAsync(addWalkRequest.RegionID);
            if(region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionID), $"{nameof(addWalkRequest.RegionID)} is invalid.");
            }

            var walk = walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walk == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"{nameof(addWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync (UpdateWalkRequest updateWalkRequest)
        {
            if(updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} cannot be empty");
            }

            var region = regionRepository.GetAsync(updateWalkRequest.RegionID);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionID), $"{nameof(updateWalkRequest.RegionID)} is invalid.");
            }

            var walk = walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walk == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
