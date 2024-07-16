using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json;
using System.Net;
using MyApi.Method;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandImgController : ControllerBase
    {
        private readonly LandOperation RentHome;
        private readonly LandImgOperation RentImgProcess;
        private readonly LandCustomerOperation rentHomeOperationCustomer;
        private readonly Cloudinary cloudinary;
        private readonly UploadImageAndGetPath uploadImageAndGetPath;

        public LandImgController(Cloudinary Cloudinary, LandImgOperation landImgOperation, LandOperation landOperation, LandCustomerOperation landCustomerOperation, UploadImageAndGetPath uploadImage)
        {
            RentImgProcess = landImgOperation;
            RentHome = landOperation;
            rentHomeOperationCustomer = landCustomerOperation;
            cloudinary = Cloudinary;
            uploadImageAndGetPath =uploadImage;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage( List<IFormFile> image)
        {
            string cloudinaryFolder = "Home/Land";
            async Task<int> GetLastId()
            {
                var items = await RentHome.GetAll();

                if (items.Data.Any())
                {
                    var lastItem = items.Data.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Land>(lastItem);
                    return RentHome.Id;
                }
                else
                {
                    return 0;
                }
            }
            var LastId = await GetLastId();

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
                    RentImgProcess.Add(new LandImg
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
        [HttpPost("{id}")]
        public async Task<IActionResult> PostUpdateImage([FromForm] List<IFormFile> image, int id)
        {
            string cloudinaryFolder = "Home/Land";
            var LastId = id;
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
                    RentImgProcess.Add(new LandImg
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
                        var RentHome = JsonSerializer.Deserialize<Land>(lastItem);

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
                var Customer = await rentHomeOperationCustomer.GetByIdList(Id);
                foreach (var item in Customer.Data)
                {
                    item.SecondStepCustomerForeignId = LastId;
                    rentHomeOperationCustomer.Update(item);
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
