using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json;
using MyApi.Method;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeImgController : ControllerBase
    {
        private readonly OfficeOperation RentHome;
        private readonly OfficeImgOperation RentImgProcess;
        private readonly OfficeCustomerOperation rentHomeOperationCustomer;
        private readonly Cloudinary cloudinary;
        private readonly UploadImageAndGetPath uploadImageAndGetPath;
        public OfficeImgController(Cloudinary Cloudinary, UploadImageAndGetPath uploadImage , OfficeImgOperation officeImgOperation, OfficeOperation officeOperation, OfficeCustomerOperation officeCustomerOperation)
        {
            RentImgProcess = officeImgOperation;
            RentHome = officeOperation;
            rentHomeOperationCustomer = officeCustomerOperation;
            cloudinary = Cloudinary;
            uploadImageAndGetPath=uploadImage;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image)
        {
            string cloudinaryFolder = "Home/Office";
            async Task<int> GetLastId()
            {
                var items = await RentHome.GetAll();
                if (items.Data.Any())
                {
                    var lastItem = items.Data.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Office>(lastItem);
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
                    RentImgProcess.Add(new OfficeImg
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
            string cloudinaryFolder = "Home/Office";
            var LastId = id;
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
                    RentImgProcess.Add(new OfficeImg
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
                        var RentHome = JsonSerializer.Deserialize<Office>(lastItem);

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
                return BadRequest($"Put img {ex}");
            }

        }
    }
}
