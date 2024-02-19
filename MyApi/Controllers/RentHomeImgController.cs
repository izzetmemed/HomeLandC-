
using Business.Abstract;
using Business.Concrete;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentHomeImgController : ControllerBase
    {
        public RentHomeOperation RentHome;
        public RentHomeOperationImg RentImgProcess;
        public ImgName ImgNameProperty;
        public RentHomeOperationCustomer rentHomeOperationCustomer;

        public RentHomeImgController()
        {
            RentImgProcess = new RentHomeOperationImg();
            ImgNameProperty = new ImgName();
            RentHome = new RentHomeOperation();
            rentHomeOperationCustomer = new RentHomeOperationCustomer();
        }


        [HttpPost]
        public  async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image)
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

            async Task<int> GetLastId()
            {
                var items = await RentHome.GetAll();

                if (items.Data.Any())
                {
                    var lastItem = items.Data.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<RentHome>(lastItem);


                   

                    return  RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId =await GetLastId();
            
                if (image == null || image.Capacity == 0)
                {
                    return BadRequest("No image provided");
                }

                try
                {
                   for (var i = 0; i < image.Count; i++)
                    {
                        string uniqueFilename = Guid.NewGuid().ToString("N");
                        string cloudinaryImagePath = $"{cloudinaryFolder}/{uniqueFilename}";

                        var uploadResult =  UploadImageAndGetPath(cloudinary, image[i], cloudinaryImagePath);
                        RentImgProcess.Add(new ImgName
                        {
                            ImgPath = cloudinaryImagePath,
                            ImgIdForeignId = LastId,
                        });

                    }
              



                return  Ok(new { Message = "Image uploaded successfully" });
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
                    string cloudinaryFolder = "Home/HomeLand";

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
                    var items = await RentHome.GetAll();

                    if (items.Data.Any())
                    {
                        var lastItem = items.Data.FirstOrDefault();
                        var RentHome = JsonSerializer.Deserialize<RentHome>(lastItem);




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
               var Customer=await rentHomeOperationCustomer.GetByIdList(Id);
                foreach (var item in Customer.Data)
                {
                    item.SecondStepCustomerForeignId = LastId;
                    rentHomeOperationCustomer.Update(item);
                }
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest("Put img");
            }
          
        }
    }
}
