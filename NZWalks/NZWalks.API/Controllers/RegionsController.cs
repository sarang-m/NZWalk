using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
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
    }
}
