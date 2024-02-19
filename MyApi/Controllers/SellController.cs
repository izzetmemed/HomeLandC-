using _00.DataAccess.AccessingDbRent.Abstract;
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
    public class SellController : ControllerBase
    {
        public SellOperation sellOperation  =new SellOperation();
        public SellOperationImg sellOperationImg = new SellOperationImg();
        public SellOperationCustomer sellOperationCustomer = new SellOperationCustomer();

        [HttpGet]
        public async Task<List<string>> Get()
        {
            try
            {
                var result = await sellOperation.GetAll();
                return result.Data;
            }
            catch
            {
                return new List<string>();
            }
           
        }
        [HttpGet("Coordinate")]
        public async Task<List<string>> GetCoordinate()
        {
            try
            {
                var result = await sellOperation.GetAllCoordinate();
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
                var result = await sellOperation.GetAllNormal();
                return result.Data;
            }catch(Exception ex) { Console.WriteLine(ex.Message);return new List<string>(); }
            
        }
        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            try
            {
                var result = await sellOperation.GetByIdList(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result.Data;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new object();
            }
            
        }
        [Authorize]
        [HttpGet("Admin/{Id}")]
        public async Task<object> GetAdmin(int Id)
        {
            try
            {
                var result = await sellOperation.GetByIdListAdmin(Id);
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
        public async void Add( Sell sell)
        {
            try
            {
                await sellOperation.Add(sell);
            }catch(Exception ex)
            {
                Console.WriteLine(ex);

            }
          
        }
        [Authorize]
        [HttpPost("Admin")]
        public async void AddOld(Sell rentHomeObject)
        {
            try
            {
                var Id = rentHomeObject.Id;
                rentHomeObject.Id = 0;
                await sellOperation.Add(rentHomeObject);
                var NewId = await GetLastId();
                var data = await sellOperationImg.GetByIdList(Id);
                foreach (var item in data.Data)
                {
                    item.ImgIdForeignId = NewId;
                    await sellOperationImg.Update(item);
                }
                var info = await sellOperationCustomer.GetByIdList(Id);
                foreach (var item in info.Data)
                {
                    item.SecondStepCustomerForeignId = NewId;
                    await sellOperationCustomer.Update(item);
                }
                var rent = await sellOperation.GetById(Id);
                await sellOperation.Delete(rent.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [Authorize]
        [HttpDelete("{Id}")]
        public async void Delete(int Id)
        {
            try
            {
                var imgList = await sellOperationImg.GetByIdList(Id);
                sellOperationImg.DeleteList(Id);
                sellOperationCustomer.DeleteList(Id);
                var entity = await sellOperation.GetById(Id);

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

                sellOperation.Delete(entity.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
           
        }
        [Authorize]
        [HttpPut]
        public void Update([FromBody] Sell sell)
        {
            try
            {
                sellOperation.Update(sell);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private async Task<int> GetLastId()
        {
            var items = await sellOperation.GetAll();

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
    }
}
