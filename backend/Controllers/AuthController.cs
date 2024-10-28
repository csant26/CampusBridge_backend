using backend.CustomActionFilter;
using backend.Models.DTO.Login;
using backend.Repository.Token;
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
        [ValidateModel]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.username);
            if (user!=null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    if (role!=null)
                    {
                        var jwtToken = await tokenRepository.CreateJWTToken(user, role);
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

        [HttpPost]
        [ValidateModel]
        [Route("Logout")]
        public async Task<IActionResult> Logout(LogoutRequestDTO logoutRequest)
        {
            var destroyedToken = await tokenRepository.DestroyJWTToken(logoutRequest.jwtToken);
            var response = new LogoutRequestDTO
            {
                jwtToken = destroyedToken
            };
            return Ok(response);
        }
    }
}
