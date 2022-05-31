using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDTO>>> GetPlatforms()
        {
            var platforms = await _platformRepository.GetAllPlatformsAsync().ConfigureAwait(false);
            var platformsDto = _mapper.Map<IEnumerable<PlatformReadDTO>>(platforms);
            return Ok(platformsDto);
        }

        [HttpGet]
        [Route("{id}", Name = nameof(GetPlatformById))]
        public async Task<ActionResult<PlatformReadDTO>> GetPlatformById(int id)
        {
            var platform = await _platformRepository.GetPlatformByIdAsync(id).ConfigureAwait(false);

            if (platform == null)
            {
                return NotFound();
            }

            var platformDTO = _mapper.Map<PlatformReadDTO>(platform);
            return Ok(platformDTO);
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            try
            {
                var platform = _mapper.Map<Platform>(platformCreateDTO);
                await _platformRepository.CreatePlatformAsync(platform).ConfigureAwait(false);
                await _platformRepository.SaveChangesAsync().ConfigureAwait(false);
                var platformReadDTO = _mapper.Map<PlatformReadDTO>(platform);
                return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDTO.Id }, platformReadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
