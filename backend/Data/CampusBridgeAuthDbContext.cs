using backend.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CampusBridgeAuthDbContext : IdentityDbContext
    {
        public CampusBridgeAuthDbContext(DbContextOptions<CampusBridgeAuthDbContext> options):base(options) { }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<AllToken> AllTokens { get; set; }
    }
}
