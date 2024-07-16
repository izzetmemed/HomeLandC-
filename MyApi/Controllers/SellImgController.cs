using Business.Abstract;
using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json;
using DataAccess.AccessingDbRent.Concrete;
using Microsoft.AspNetCore.Authorization;
using MyApi.Method;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellImgController : ControllerBase
    {
        private readonly SellOperationImg RentImgProcess;
        private readonly SellOperation sell;
        private readonly SellOperationCustomer sellOperationCustomer;
        private readonly Cloudinary cloudinary;
        private readonly UploadImageAndGetPath uploadImageAndGetPath;
        public SellImgController(Cloudinary Cloudinary , UploadImageAndGetPath uploadImage, SellOperationImg sellOperationImg , SellOperation sellOperation, SellOperationCustomer sellOperationCustom)
        {
            RentImgProcess = sellOperationImg;
            sell = sellOperation;
            sellOperationCustomer = sellOperationCustom;
            cloudinary = Cloudinary;
            uploadImageAndGetPath=uploadImage;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image)
        {
            string cloudinaryFolder = "Home/Sell";
            async Task<int> GetLastId()
            {
                var items = await sell.GetAll();

                if (items.Data.Any())
                {
                    var lastItem = items.Data.FirstOrDefault();
                    var RentHome = JsonSerializer.Deserialize<Sell>(lastItem);
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
                        RentImgProcess.Add(new SellImg
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
        public async Task<IActionResult> PostUpdateImage([FromForm] List<IFormFile> image, int id)
        {
            string cloudinaryFolder = "Home/Sell";
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
                        RentImgProcess.Add(new SellImg
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
        [HttpPost("Video/{id}")]
        public async Task<IActionResult> AddVideo(IFormFile video, int id)
        {
            string cloudinaryFolder = "Home/SellVideo";
            {
                if (video == null)
                {
                    return BadRequest("No image provided");
                }
                try
                { 
                    string uniqueFilename = Guid.NewGuid().ToString("N");
                    string cloudinaryVideoPath = $"{cloudinaryFolder}/{uniqueFilename}";
                    var dataSell=await sell.GetById(id);
                    if (dataSell == null)
                    {
                        return BadRequest("No image provided");
                    }
                    dataSell.Data.VideoPath = cloudinaryVideoPath;
                    sell.Update(dataSell.Data);
                    var uploadResult = await UploadVideoAndGetPath(cloudinary, video, cloudinaryVideoPath);
                    return Ok(new { Message = "Image uploaded successfully" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
        private async Task<UploadResult> UploadVideoAndGetPath(Cloudinary cloudinary, IFormFile video, string cloudinaryVideoPath)
        {
            using (var stream = video.OpenReadStream())
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(video.FileName, stream),
                    PublicId = cloudinaryVideoPath,
                };

                return await cloudinary.UploadAsync(uploadParams);
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
                        var RentHome = JsonSerializer.Deserialize<Sell>(lastItem);




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
                var Customer = await sellOperationCustomer.GetByIdList(Id);
                foreach (var item in Customer.Data)
                {
                    item.SecondStepCustomerForeignId = LastId;
                    sellOperationCustomer.Update(item);
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
