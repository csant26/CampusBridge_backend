using backend.Models.Domain.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Repository.Token
{
    public interface ITokenRepository
    {
        public Task<string> CreateJWTToken(IdentityUser user, string role);
        public Task<string> DestroyJWTToken(string token);
    }
}
