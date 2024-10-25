using backend.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Repository.Interface
{
    public interface ITokenRepository
    {
        public Task<string> CreateJWTToken(IdentityUser user, string role);
        public Task<AllToken> DestroyJWTToken(string token);
    }
}
