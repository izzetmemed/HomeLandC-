using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using _00.DataAccess.AccessingDbRent.Abstract;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObyektImgController : ControllerBase
    {
        public ObyektOperationImg RentImgProcess;
        public ObyektImg ImgNameProperty;
        public ObyektOperation sell;
        public ObyektOperationCustomer customerObyekt;

        public ObyektImgController()
        {
            RentImgProcess = new ObyektOperationImg();
            ImgNameProperty = new ObyektImg();
            sell = new ObyektOperation();
            customerObyekt=new ObyektOperationCustomer();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image)
        {
            try
            {

            }
            catch
            {

            }
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .Build();

            string cloudName = configuration["Password:CloudName"];
            string apiKey = configuration["Password:ApiKey"];
            string apiSecret = configuration["Password:ApiSecret"];
            string cloudinaryFolder = "Home/Obyekt";

            var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            var cloudinary = new Cloudinary(cloudinaryAccount);
            async Task<int> GetLastId()
            {
                var items = await sell.GetAll();

                if (items.Data.Any())
                {
                    var lastItem = items.Data.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Obyekt>(lastItem);




                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();
            {
                if (image == null || image.Count == 0)
                {
                    return BadRequest("No image provided");
                }

                try
                {
                    for (var i = 0; i < image.Count; i++)
                    {
                        string uniqueFilename = Guid.NewGuid().ToString("N");
                        string cloudinaryImagePath = $"{cloudinaryFolder}/{uniqueFilename}";

                        var uploadResult = UploadImageAndGetPath(cloudinary, image[i], cloudinaryImagePath);
                        RentImgProcess.Add(new ObyektImg
                        {
                            ImgPath = cloudinaryImagePath,
                            ImgIdForeignId = LastId,
                        });

                    }




                    return Ok(new { Message = "Image uploaded successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }


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

        [HttpGet("DownloadImages")]
        public IActionResult DownloadImages([FromQuery(Name = "imgNames")] List<string> DownloadImages)
        {
            try
            {
                if (DownloadImages[0] == null)
                {
                    return Ok("Array is null");
                }
                else
                {
                    var SplitDataDownloadImages = DownloadImages[0].Split(",");
                    IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();

                    string cloudName = configuration["Password:CloudName"];
                    string cloudinaryFolder = "Home/Obyekt";

                    List<string> imageUrls = new List<string>();
                    foreach (var imgName in SplitDataDownloadImages)
                    {
                        string imageUrl = $" https://res.cloudinary.com/{cloudName}/image/upload/c_scale,q_auto,f_auto/{imgName}";
                        imageUrls.Add(imageUrl);
                    }

                    return Ok(new { Message = "Image URLs generated successfully", ImageUrls = imageUrls });
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            try
            {
                var ListImg = await RentImgProcess.GetByIdList(Id);
                async Task<int> GetLastId()
                {
                    var items = await sell.GetAll();

                    if (items.Data.Any())
                    {
                        var lastItem = items.Data.FirstOrDefault();
                        var RentHome = JsonSerializer.Deserialize<Obyekt>(lastItem);
                        return RentHome.Id;
                    }
                    else
                    {
                        return 0;
                    }
                }
                var LastId = await GetLastId();
                foreach (var item in ListImg.Data)
                {
                    item.ImgIdForeignId = LastId;
                    RentImgProcess.Update(item);
                }
                var Customer = await customerObyekt.GetByIdList(Id);
                foreach (var item in Customer.Data)
                {
                    item.SecondStepCustomerForeignId = LastId;
                    customerObyekt.Update(item);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Put img");
            }

        }
    }
}
