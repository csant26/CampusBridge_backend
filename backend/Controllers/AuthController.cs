using backend.Models.DTO;
using backend.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, 
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.username);
            if (user!=null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.password);

                if (checkPasswordResult)
                {
                    var role = await userManager.GetRolesAsync(user);
                    if (role != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user,role.ToString());
                        var response = new LoginResponseDTO
                        {
                            jwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest("No roles assigned to access resources.");
                    }
                }
                else
                {
                    return BadRequest("Incorrect Password.");
                }
            }
            else
            {
                return BadRequest("User doesn't exist.");
            }
        } 
    }
}
