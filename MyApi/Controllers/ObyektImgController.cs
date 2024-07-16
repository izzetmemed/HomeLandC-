using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using _00.DataAccess.AccessingDbRent.Abstract;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.Extensions.Logging;
using MyApi.Method;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObyektImgController : ControllerBase
    {
        private readonly ObyektOperationImg RentImgProcess;
        private readonly ObyektOperation sell;
        private readonly ObyektOperationCustomer customerObyekt;
        private readonly Cloudinary cloudinary;
        private readonly UploadImageAndGetPath uploadImageAndGetPath;

        public ObyektImgController(Cloudinary Cloudinary, ObyektOperationImg obyektOperationImg, ObyektOperation obyektOperation, ObyektOperationCustomer obyektOperationCustomer,  UploadImageAndGetPath uploadImage)
        {
            RentImgProcess = obyektOperationImg;
            sell = obyektOperation;
            cloudinary = Cloudinary;
            customerObyekt = obyektOperationCustomer;
            uploadImageAndGetPath = uploadImage;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image)
        {
            string cloudinaryFolder = "Home/Obyekt";
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

                        var uploadResult = uploadImageAndGetPath.UploadImage(cloudinary, image[i], cloudinaryImagePath);
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
        [HttpPost("{id}")]
        public async Task<IActionResult> PostUpdateImage([FromForm] List<IFormFile> image,int id)
        {
            string cloudinaryFolder = "Home/Obyekt";
            var LastId = id;
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

                        var uploadResult = uploadImageAndGetPath.UploadImage(cloudinary, image[i], cloudinaryImagePath);
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
