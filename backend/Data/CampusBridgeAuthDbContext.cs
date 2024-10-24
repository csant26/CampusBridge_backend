using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CampusBridgeAuthDbContext : IdentityDbContext
    {
        public CampusBridgeAuthDbContext(DbContextOptions<CampusBridgeAuthDbContext> options) : base(options) { }
    }
}
