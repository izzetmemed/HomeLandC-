using Business.Abstract;
using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObyektController : ControllerBase
    {
        public ObyektOperationCustomer RentCustomerProcess { get; set; }
        public ObyektOperation RentProcess { get; set; }
        public ObyektOperationImg RentImgProcess { get; set; }

        public ObyektController()
        {
            RentCustomerProcess = new ObyektOperationCustomer();
            RentProcess = new ObyektOperation();
            RentImgProcess = new ObyektOperationImg();
        }
        [HttpGet]
        public async Task<List<string>> Get()
        {
            try
            {
                var result = await RentProcess.GetAll();
                return result.Data;
            }
            catch
            {
                return  new List<string>();
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
        [Authorize]
        [HttpGet("Normal")]
        public async Task<List<string>> GetNormal()
        {
            try
            {
                var result = await RentProcess.GetAllNormal();
                return result.Data;
            }
            catch
            {
                return new List<string>();
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
            catch
            {
                return new object();
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
        public async void Add( Obyekt entity)
        {
            try
            {
                await RentProcess.Add(entity);
            }catch (Exception ex) { Console.WriteLine(ex); }
          
        }
        [Authorize]
        [HttpPost("Admin")]
        public async void AddOld(Obyekt rentHomeObject)
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
        public async void Update( Obyekt entity)
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
                await RentCustomerProcess.DeleteList(Id);
                var entity = await RentProcess.GetById(Id);

                try
                {


                    IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                    string cloudName = configuration["Password:CloudName"];
                    string apiKey = configuration["Password:ApiKey"];
                    string apiSecret = configuration["Password:ApiSecret"];

                    var cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
                    var cloudinary = new Cloudinary(cloudinaryAccount);

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
            catch (Exception ex) { Console.WriteLine(ex); }
           
        }

        private async Task<int> GetLastId()
        {
            var items = await RentProcess.GetAll();

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
    }
}
