
using Business.Abstract;
using Business.Concrete;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using MyApi.Method;
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
        private readonly RentHomeOperation RentHome;
        private readonly RentHomeOperationImg RentImgProcess;
        private readonly RentHomeOperationCustomer RentHomeOperationCustomer;
        private readonly Cloudinary cloudinary;
        private readonly UploadImageAndGetPath uploadImageAndGetPath;
        public RentHomeImgController(Cloudinary Cloudinary , UploadImageAndGetPath uploadImage, RentHomeOperationImg rentHomeOperationImg , RentHomeOperation rentHomeOperation , RentHomeOperationCustomer rentHomeOperationCustomer)
        {
            RentImgProcess = rentHomeOperationImg;
            RentHome = rentHomeOperation;
            RentHomeOperationCustomer = rentHomeOperationCustomer;
            cloudinary = Cloudinary;
            uploadImageAndGetPath = uploadImage;
        }


        [HttpPost]
        public  async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image)
        {
            string cloudinaryFolder = "Home/HomeLand";
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

                        var uploadResult = uploadImageAndGetPath.UploadImage(cloudinary, image[i], cloudinaryImagePath);
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
        [HttpPost("{id}")]
        public  async Task<IActionResult> PostUpdateImage([FromForm] List<IFormFile> image, int id)
        {
            string cloudinaryFolder = "Home/HomeLand";
            var LastId =id;
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

                        var uploadResult = uploadImageAndGetPath.UploadImage(cloudinary, image[i], cloudinaryImagePath);
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
               var Customer=await RentHomeOperationCustomer.GetByIdList(Id);
                foreach (var item in Customer.Data)
                {
                    item.SecondStepCustomerForeignId = LastId;
                    RentHomeOperationCustomer.Update(item);
                }
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest($"Put img{ex}");
            }
          
        }
    }
}
