using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfile: Profile
    {
        public RegionProfile()
        {
            CreateMap<Model.Domain.Region,Model.DTO.Region>();
        }
    }
}
