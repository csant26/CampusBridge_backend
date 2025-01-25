using backend.CustomActionFilter;
using backend.Data;
using backend.Models.DTO.Auth;
using backend.Models.DTO.Login;
using backend.Repository.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly CampusBridgeDbContext campusBridgeDbContext;

        public AuthController(UserManager<IdentityUser> userManager, 
            ITokenRepository tokenRepository,
            CampusBridgeDbContext campusBridgeDbContext)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.campusBridgeDbContext = campusBridgeDbContext;
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
                            jwtToken = jwtToken,
                            role = roles,
                            Id = loginRequest.username
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
        [HttpGet("GetNameFromId")]
        [ValidateModel]
        public async Task<IActionResult> GetDataFromId(string id)
        {
            AccumulatedData data = new AccumulatedData();
            var existingUser = await userManager.FindByEmailAsync(id);
            if (existingUser == null) { return BadRequest("no user"); }
            var role = await userManager.GetRolesAsync(existingUser);
            if (role.Contains("Univeristy"))
                data.Role = "University";
            if (role.Contains("College"))
            {
                var college = await campusBridgeDbContext.Colleges.FirstOrDefaultAsync(x => x.Email == id);
                if (college == null) { return BadRequest(); }
                data.Name = college.Name;
                data.Role = "College";
            }
            if (role.Contains("Teacher"))
            {
                var teacher = await campusBridgeDbContext.Teachers.FirstOrDefaultAsync(x => x.Email == id);
                if (teacher == null) { return BadRequest(); }
                data.Name = teacher.Name;
                data.Role = "Teacher";
            }
            if (role.Contains("Student"))
            {
                var student = await campusBridgeDbContext.Students.FirstOrDefaultAsync(x => x.Email == id);
                if (student == null) { return BadRequest(); }
                data.Name=student.Name;
                data.Role="Student";
            }
            return Ok(data);
        }
    }
}
