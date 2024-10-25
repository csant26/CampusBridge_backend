using backend.CustomActionFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        [HttpPost]
        [ValidateModel]
        //public async Task<IActionResult> CreateStudent()
        //{
            
        //}
    }
}
