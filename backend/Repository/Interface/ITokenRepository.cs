using Microsoft.AspNetCore.Identity;

namespace backend.Repository.Interface
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, string role);
    }
}
