using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        IEnumerable<Platform> GetAllPlatforms();
        Task<IEnumerable<Platform>> GetAllPlatformsAsync();

        Platform GetPlatformById(int id);
        Task<Platform> GetPlatformByIdAsync(int id);

        void CreatePlatform(Platform platform);
        Task CreatePlatformAsync(Platform platform);
    }
}
