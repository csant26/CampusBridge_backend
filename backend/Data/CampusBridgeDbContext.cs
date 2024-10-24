using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CampusBridgeDbContext : DbContext
    {
        public CampusBridgeDbContext(DbContextOptions<CampusBridgeDbContext> options):base(options) { }
    }
}
