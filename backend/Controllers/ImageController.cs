using backend.CustomActionFilter;
using backend.Images;
using backend.Migrations.CampusBridgeDb;
using backend.Models.Domain.Content.Images;
using backend.Models.DTO.Content.Images;
using backend.Repository.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageHandling imageHandling;

        public ImageController(ImageHandling imageHandling)
        {
            this.imageHandling = imageHandling;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ValidateModel]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var imagePath = await imageHandling.UploadImage(imageUploadRequestDTO);
            if (imagePath == null) { return BadRequest("Image couldn't be created."); }
            return Ok(imagePath);
        }
    }
}
