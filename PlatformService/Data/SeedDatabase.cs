using PlatformService.Models;

namespace PlatformService.Data
{
    public static class SeedDatabase
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (dbContext == null)
                {
                    throw new Exception($"Unable to get service {nameof(AppDbContext)}.");
                }

                Console.WriteLine("Starting Database Seed");

                if (dbContext.Platforms.Any())
                {
                    Console.WriteLine("Database Seed was canceled since there is already data.");
                    return;
                }
                else
                {
                    AddPlatforms(dbContext);
                    dbContext.SaveChanges();
                    Console.WriteLine("Database Seed completed.");
                }
            }
        }

        private static void AddPlatforms(AppDbContext dbContext)
        {
            var platforms = new List<Platform>()
            {
                new Platform { Name = "Dot Net", Publisher = "Microsfot", Cost = "Free"},
                new Platform { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free"},
                new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free"}
            };

            dbContext.Platforms.AddRange(platforms);
        }
    }
}
