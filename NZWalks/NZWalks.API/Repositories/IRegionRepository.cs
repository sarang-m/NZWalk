﻿using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionAsync();
        Task<Region> GetRegionAsync(Guid id);
        Task<Region> GetRegionByCodeAsync(string code);

        Task<Region> AddRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid id);

        Task<Region> UpdateRegionAsync(Guid id, Region region);


    }
}
