using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await regionRepository.GetAllRegionAsync();

            //return region DTO
            //var regionsDTO = new List<Model.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Model.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population

            //    };
            //    regionsDTO.Add(regionDTO);
            //});

            var regionsDTO = mapper.Map<List<Model.DTO.Region>>(regions);

            return Ok(regionsDTO);
            
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionByIdAsync")]
        public async Task<IActionResult> GetRegionByIdAsync(Guid id)
        {
            var region = await regionRepository.GetRegionAsync(id);
            if (region is null)
            {
                return NotFound();
            }
            mapper.Map<Model.DTO.Region>(region);
            return Ok(region);
        }
        [HttpGet]
        [Route("{code}")]
        
        public async Task<IActionResult> GetRegionByCodeAsyc(string code)
        {
            var region = await regionRepository.GetRegionByCodeAsync(code);
            if (region is null)
            {
                return NotFound();
            }
            return Ok(region);
        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {

            //Validate AddRegion Method
            bool isValid = ValidateAddRegion(addRegionRequest);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            //Request(DTO) to Domain Model
            var region = new Model.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,
            };

            //Passs details to Repository
            var response = await regionRepository.AddRegionAsync(region);

            //Convert back to DTO
            var regionDTO = new Model.DTO.Region()
            {
                Id = response.Id,
                Code = response.Code,
                Area = response.Area,
                Lat = response.Lat,
                Long = response.Long,
                Name = response.Name,
                Population = response.Population,
            };

            return CreatedAtAction(nameof(GetRegionByIdAsync),new {Id =regionDTO.Id }, regionDTO);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);
            if (region is null)
            {
                return NotFound();
            }
            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,

            };

            //mapper.Map<Model.DTO.Region>(region);
            return Ok(regionDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]Model.DTO.UpdateRegionRequest updateRegionRequest)
        {
            var region = new Model.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population,
            };

            region = await regionRepository.UpdateRegionAsync(id, region);
            if (region is null)
            {
                return NotFound();
            }

            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population,

            };

            return Ok(regionDTO);


        }

        private bool ValidateAddRegion(AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), " Region data is InvalidCastException");
                return false;

            }
            if (string.IsNullOrEmpty(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), "Invalid Code");
            }
            if (string.IsNullOrEmpty(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), "Invalid Name");
            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), "Invalid Area Code");
            }
            if (double.IsFinite(addRegionRequest.Lat) && Math.Abs(addRegionRequest.Lat) <= 90)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat), "Invalid Latitude");
            }
            if (double.IsFinite(addRegionRequest.Long) && Math.Abs(addRegionRequest.Long) <= 180)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long), "Invalid Longitude");
            }
            if (addRegionRequest.Population <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), "Invalid Population");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
    }
}
