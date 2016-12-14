
using Microsoft.EntityFrameworkCore;

namespace cityInfo.Entities
{
    public class CityInfoContext: DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options)
        {
            Database.EnsureCreated();
        }
        public DbSet<City> cities {get; set;}
        public DbSet<PointOfInterest> pointsOfInterest {get; set;}
    }
}