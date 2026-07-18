using backend.Models.Domain.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CampusBridgeAuthDbContext : IdentityDbContext
    {
        public CampusBridgeAuthDbContext(DbContextOptions<CampusBridgeAuthDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("aspnetusers");
            builder.Entity<IdentityRole>().ToTable("aspnetroles");

            builder.Entity<IdentityUserRole<string>>().ToTable("aspnetuserroles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("aspnetuserclaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("aspnetuserlogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("aspnetroleclaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("aspnetusertokens");

            builder.Entity<AllToken>().ToTable("all_tokens");
            builder.Entity<RevokedToken>().ToTable("revoked_tokens");
        }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<AllToken> AllTokens { get; set; }
    }
}
