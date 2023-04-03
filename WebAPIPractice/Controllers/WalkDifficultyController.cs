using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIPractice.Models.Domain;
using WebAPIPractice.Models.Domain.DTO;
using WebAPIPractice.Repositories;

namespace WebAPIPractice.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.difficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDifficultyAsync()
        {
            var walkDifficulty = await difficultyRepository.GetAllAsync();
            var walkDifficultyDTO = mapper.Map<List<Models.Domain.DTO.WalksDifficultyDTO>>(walkDifficulty);
            return Ok(walkDifficultyDTO);
        }

        [HttpGet]
        [ActionName("GetDifficultyAsync")]
        public async Task<IActionResult> GetDifficultyAsync([FromQuery] Guid Id)
        {
            var difficultyRecord = await difficultyRepository.GetAsync(Id);
            mapper.Map<Models.Domain.DTO.WalksDifficultyDTO>(difficultyRecord);
            return Ok(difficultyRecord);
        }

        [HttpPost]
        public async Task<IActionResult> AddDifficultyAsync([FromBody] AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficulty = new WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code,
            };

            var newDifficulty = await difficultyRepository.AddAsync(walkDifficulty);
            if(newDifficulty == null)
            {
                return NotFound();
            }

            var difficultyDTO = new WalksDifficultyDTO()
            {
                Code = newDifficulty.Code,
            };
            return CreatedAtAction(nameof(GetDifficultyAsync), new { id = newDifficulty.Id }, difficultyDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDifficultyAsync([FromQuery] Guid Id, UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var difficultyDomain = new WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code,
            };

            difficultyDomain = await difficultyRepository.UpdateAsync(Id, difficultyDomain);
            if(difficultyDomain == null)
            {
                return NotFound();
            }

            var difficultyDTO = mapper.Map<WalksDifficultyDTO>(difficultyDomain);
            return Ok (difficultyDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDifficultyAsync([FromQuery] Guid Id)
        {
            var difficultyDomain = await difficultyRepository.DeleteAsync(Id);
            if(difficultyDomain == null)
            {
                return NotFound();
            }
            return Ok(difficultyDomain);
        }
    }
}
