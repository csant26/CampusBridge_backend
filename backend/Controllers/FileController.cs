using backend.CustomActionFilter;
using backend.Files;
using backend.Migrations.CampusBridgeDb;
using backend.Models.DTO.Content.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileHandling fileHandling;

        public FileController(FileHandling fileHandling)
        {
            this.fileHandling = fileHandling;
        }
        [HttpPost("UploadFile")]
        [Consumes("multipart/form-data")]
        [ValidateModel]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequestDTO fileUploadRequestDTO)
        {
            var filePath = await fileHandling.UploadFile(fileUploadRequestDTO);
            if (filePath == null) { return BadRequest("File couldn't be uploaded."); }
            return Ok(filePath);
        }
    }
}
