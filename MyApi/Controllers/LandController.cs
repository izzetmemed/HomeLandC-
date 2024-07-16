using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Model.DTOmodels;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandController : ControllerBase
    {
        public LandCustomerOperation RentCustomerProcess { get; set; }
        public LandOperation RentProcess { get; set; }
        public LandImgOperation RentImgProcess { get; set; }
        public Cloudinary cloudinary { get; set; }

        public LandController(Cloudinary cloudinaryNew, LandCustomerOperation landCustomerOperation, LandOperation landOperation, LandImgOperation landImgOperation)
        {
            RentCustomerProcess = landCustomerOperation;
            RentProcess = landOperation;
            RentImgProcess = landImgOperation;
            cloudinary = cloudinaryNew;
        }
        [HttpPost("GetAll")]
        public async Task<List<string>> Get(List<int> ids)
        {
            try
            {
                var result = await RentProcess.GetAll(x => ids.Contains(x.Id));
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<string>();
            }

        }
        [HttpGet("Page-{Page}")]
        public async Task<SearchDTO> GetPage(int Page)
        {
            try
            {
                var result = await RentProcess.GetAllPage(Page);
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SearchDTO();
            }

        }
        [HttpPost("Search-{Page}")]
        public async Task<SearchDTO> GetAllSearch(int Page, SearchModel searchModel)
        {
            try
            {
                var result = await RentProcess.GetAllSearch(searchModel, Page);
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SearchDTO();
            }
        }
        [Authorize]
        [HttpGet("AdminId-{id}")]
        public async Task<SearchDTO> GetAllId(int id)
        {
            try
            {
                var result = await RentProcess.GetAllId(id);
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SearchDTO();
            }
        }
        [Authorize]
        [HttpGet("AdminONumber-{ONumber}")]
        public async Task<SearchDTO> GetAllONumber(string ONumber)
        {
            try
            {
                var result = await RentProcess.GetAllOwnNumber(ONumber);
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SearchDTO();
            }

        }
        [Authorize]
        [HttpGet("AdminCNumber-{CNumber}")]
        public async Task<SearchDTO> GetAllCnumber(string CNumber)
        {
            try
            {
                var result = await RentProcess.GetAllCustomerNumber( CNumber);
             
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SearchDTO();
            }

        }
        [Authorize]
        [HttpGet("Normal-{Page}")]
        public async Task<SearchDTO> GetNormal(int Page)
        {
            try
            {
                var result = await RentProcess.GetAllNormal(Page);
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new SearchDTO();
            }

        }

        [HttpGet("Coordinate")]
        public async Task<List<string>> GetCoordinate()
        {
            try
            {
                var result = await RentProcess.GetAllCoordinate();
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<string>();
            }

        }
        [HttpGet("Recommend")]
        public async Task<List<string>> GetRecommend()
        {
            try
            {
                var result = await RentProcess.GetAllRecommend();
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<string>();
            }

        }
        [Authorize]
        [HttpPut("Recommend")]
        public async void UpdateRecommend([FromBody] Land sell)
        {
            try
            {
                var items = await RentProcess.GetAllRecommend();
                if (items.Data.Count > 30)
                {
                    var lastItem = items.Data.LastOrDefault();
                    var Sell30 = JsonSerializer.Deserialize<Land>(lastItem);
                    var LastId = await RentProcess.GetByIdListAdmin(Sell30.Id);
                    var LastIdString = JsonSerializer.Deserialize<Land>(JsonSerializer.Serialize(LastId.Data));
                    LastIdString.Recommend = false;
                    RentProcess.Update(LastIdString);
                }
                RentProcess.Update(sell);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            try
            {
                var result = await RentProcess.GetByIdList(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<string>();
            }

        }
        [Authorize]
        [HttpGet("Admin/{Id}")]
        public async Task<object> GetAdmin(int Id)
        {
            try
            {
                var result = await RentProcess.GetByIdListAdmin(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }

        }

        [HttpPost]
        public async void Add(Land rentHomeObject)
        {
            try
            {
                await RentProcess.Add(rentHomeObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [Authorize]
        [HttpPost("Admin")]
        public async void AddOld(Land rentHomeObject)
        {
            try
            {
                var Id = rentHomeObject.Id;
                rentHomeObject.Id = 0;
                await RentProcess.Add(rentHomeObject);
                var NewId = await GetLastId();
                var data = await RentImgProcess.GetByIdList(Id);
                foreach (var item in data.Data)
                {
                    item.ImgIdForeignId = NewId;
                    await RentImgProcess.Update(item);
                }
                var info = await RentCustomerProcess.GetByIdList(Id);
                foreach (var item in info.Data)
                {
                    item.SecondStepCustomerForeignId = NewId;
                    await RentCustomerProcess.Update(item);
                }
                var rent = await RentProcess.GetById(Id);
                await RentProcess.Delete(rent.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [Authorize]
        [HttpPut]
        public async void Update([FromBody] Land entity)
        {
            try
            {
                await RentProcess.Update(entity);
            }
            catch (Exception ex) { Console.WriteLine(ex); }

        }
        [Authorize]
        [HttpDelete("{Id}")]
        public async void Delete(int Id)
        {
            try
            {
                var imgList = await RentImgProcess.GetByIdList(Id);
                RentImgProcess.DeleteList(Id);
                RentCustomerProcess.DeleteList(Id);
                var entity = await RentProcess.GetById(Id);
                if (entity.Data == null)
                {
                    return;
                }
                try
                {

                    var publicIds = imgList.Data.Select(x => x.ImgPath).ToList();

                    DelResParams deleteParams = new DelResParams()
                    {
                        PublicIds = publicIds
                    };

                    DelResResult result = cloudinary.DeleteResources(deleteParams);

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Images deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete images. Error: " + result.Error.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                RentProcess.Delete(entity.Data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        private async Task<int> GetLastId()
        {
            var items = await RentProcess.GetAll();
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
    }
}
