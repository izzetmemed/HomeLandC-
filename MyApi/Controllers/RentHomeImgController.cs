
using Business.Abstract;
using Business.Concrete;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentHomeImgController : ControllerBase
    {

        public IRentHomeServiceImg RentImgProcess;
        public ImgName ImgNameProperty;

        public RentHomeImgController()
        {
            RentImgProcess = new RentHomeOperationImg();
            ImgNameProperty = new ImgName();
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
                string cloudinaryFolder = "Home/HomeLand";

                var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
                var cloudinary = new Cloudinary(cloudinaryAccount);

                string uniqueFilename = Guid.NewGuid().ToString("N");
                string cloudinaryImagePath = $"{cloudinaryFolder}/{uniqueFilename}";

                var uploadResult = UploadImageAndGetPath(cloudinary, image, cloudinaryImagePath);



                RentImgProcess.Add(new ImgName
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
        [HttpGet("{uniqueFilenameList}")]
        public IActionResult DownLoadImage(List<string> uniqueFilenameList)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string cloudName = configuration["Password:CloudName"];
                string apiKey = configuration["Password:ApiKey"];
                string apiSecret = configuration["Password:ApiSecret"];
                string cloudinaryFolder = "Home/HomeLand";

                var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
                var cloudinary = new Cloudinary(cloudinaryAccount);

                List<string> imageUrls = new List<string>();

                foreach (var item in uniqueFilenameList)
                {
                    string cloudinaryImagePath = $"{cloudinaryFolder}/{item}";
                    var imageUrl = cloudinary.Api.UrlImgUp.Transform(new Transformation().FetchFormat("auto")).BuildUrl(cloudinaryImagePath);
                    imageUrls.Add(imageUrl);
                }

                // Do something with the imageUrls, such as returning them in the response
                return Ok(new { Message = "Image downloaded successfully", ImageUrls = imageUrls });
            }
            catch
            {
                return BadRequest("Image didn't download");
            }
        }

    }
}
