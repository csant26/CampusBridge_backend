using AutoMapper.Configuration;
using backend.Data;
using backend.Models.Domain.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

            //Approach 1, Step 1: Adding the freshly created tokens in the AllTokens table.
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
        public async Task<string> DestroyJWTToken(string token)
        {
            //Approach 1, Step 2: Updating the expiry date of the tokens in the AllTokens table.
            var expiredToken = await campusBridgeAuthDbContext.AllTokens
                .FirstOrDefaultAsync(t => t.Token == token);
            if (expiredToken != null)
            {
                expiredToken.ExpiresAt = DateTime.Now;
            }
            await campusBridgeAuthDbContext.SaveChangesAsync();

            //Approach 2: Adding the revoked tokens to a separate RevokedTokens table.
            var revokedToken = await campusBridgeAuthDbContext.RevokedTokens
                .FirstOrDefaultAsync(t=>t.Token==token);
            if (revokedToken == null)
            {
                await campusBridgeAuthDbContext.RevokedTokens.AddAsync(new RevokedToken
                {
                    Token = token,
                    RevokedAt = DateTime.Now
                });
            }
            await campusBridgeAuthDbContext.SaveChangesAsync();

            if (expiredToken != null)
            {
                return expiredToken.Token;
            }
            else
            {
                return null;
            }
        }
    }
}
