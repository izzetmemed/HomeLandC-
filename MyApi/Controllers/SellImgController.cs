using Business.Abstract;
using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellImgController : ControllerBase
    {
        public SellOperationImg RentImgProcess;
        public SellImg ImgNameProperty;

        public SellImgController()
        {
            RentImgProcess = new SellOperationImg();
            ImgNameProperty = new SellImg();
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image provided");
            }

            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

                string cloudName = configuration["Password:CloudName"];
                string apiKey = configuration["Password:ApiKey"];
                string apiSecret = configuration["Password:ApiSecret"];
                string cloudinaryFolder = "Home/Sell";

                var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
                var cloudinary = new Cloudinary(cloudinaryAccount);

                string uniqueFilename = Guid.NewGuid().ToString("N");
                string cloudinaryImagePath = $"{cloudinaryFolder}/{uniqueFilename}";

                var uploadResult = UploadImageAndGetPath(cloudinary, image, cloudinaryImagePath);



                RentImgProcess.Add(new SellImg
                {
                    ImgPath = cloudinaryImagePath,
                    ImgIdForeignId = 1,
                });

                return Ok(new { Message = "Image uploaded successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        static string UploadImageAndGetPath(Cloudinary cloudinary, IFormFile image, string cloudinaryImagePath)
        {
            using (var stream = image.OpenReadStream())
            {
                cloudinaryImagePath = cloudinaryImagePath.TrimStart('/');

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    PublicId = cloudinaryImagePath,
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                return uploadResult.SecureUri.ToString();
            }
        }
    }
}
