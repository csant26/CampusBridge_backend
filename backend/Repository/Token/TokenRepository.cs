using backend.Data;
using backend.Models.Domain.Token;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Repository.Token
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly CampusBridgeAuthDbContext campusBridgeAuthDbContext;

        public TokenRepository(IConfiguration configuration,
            CampusBridgeAuthDbContext campusBridgeAuthDbContext)
        {
            this.configuration = configuration;
            this.campusBridgeAuthDbContext = campusBridgeAuthDbContext;
        }
        public async Task<string> CreateJWTToken(IdentityUser user, string role)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );
            var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var existingToken = await campusBridgeAuthDbContext.AllTokens.FirstOrDefaultAsync(t => t.Token == jwtTokenString);
            if (existingToken == null)
            {
                await campusBridgeAuthDbContext.AllTokens.AddAsync(new AllToken
                {
                    Token = jwtTokenString,
                    ExpiresAt = DateTime.Now.AddDays(7)
                });
            }
            await campusBridgeAuthDbContext.SaveChangesAsync();

            return jwtTokenString;
        }
        public async Task<AllToken> DestroyJWTToken(string token)
        {
            var expiredToken = await campusBridgeAuthDbContext.AllTokens
                .FirstOrDefaultAsync(t => t.Token == token);
            if (expiredToken != null)
            {
                expiredToken.ExpiresAt = DateTime.Now.AddDays(-7);
            }
            await campusBridgeAuthDbContext.SaveChangesAsync();
            return expiredToken;
        }
    }
}
