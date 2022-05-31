using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _dbContext;

        public PlatformRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public void CreatePlatform(Platform platform)
        {
            // TODO: improve validation?

            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            _dbContext.Platforms.Add(platform);
        }

        public async Task CreatePlatformAsync(Platform platform)
        {
            // TODO: improve validation?

            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            await _dbContext.Platforms.AddAsync(platform); // TODO: try to return the Task and not await
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _dbContext.Platforms.ToList();
        }

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _dbContext.Platforms.ToListAsync();
        }

        public Platform GetPlatformById(int id)
        {
            return _dbContext.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Platform> GetPlatformByIdAsync(int id)
        {
            return await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            int entries = _dbContext.SaveChanges();
            return entries > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            int entries = await _dbContext.SaveChangesAsync();
            return entries > 0;
        }
    }
}
