using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionAsync();
        Task<Region> GetRegionAsync(Guid id);
        Task<Region> GetRegionByCodeAsync(string code);

    }
}
