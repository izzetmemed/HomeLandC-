using _00.DataAccess.AccessingDbRent.Abstract;
using Business.Concrete;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json;
using System.Linq;
using Model.DTOmodels;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellController : ControllerBase
    {
        public SellOperation sellOperation ;
        public SellOperationImg sellOperationImg ;
        public SellOperationCustomer sellOperationCustomer ;
        public Cloudinary cloudinary { get; set; }
        public SellController(Cloudinary cloudinaryNew, SellOperation SellOperation, SellOperationImg SellOperationImg, SellOperationCustomer SellOperationCustomer)
        {
            sellOperation = SellOperation;
            sellOperationImg = SellOperationImg;
            sellOperationCustomer = SellOperationCustomer;
            cloudinary = cloudinaryNew;
        }
        [HttpPost("GetAll")]
        public async Task<List<string>> Get(List<int> ids)
        {
            try
            {
                var result = await sellOperation.GetAll(x => ids.Contains(x.Id));
                return result.Data;
            }
            catch
            {
                return new List<string>();
            }
           
        }
        [HttpGet("Page-{Page}")]
        public async Task<SearchDTO> GetPage(int Page)
        {
            try
            {
                var result = await sellOperation.GetAllPage(Page);
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
                var result = await sellOperation.GetAllSearch(searchModel, Page);
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SearchDTO();
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
        [HttpGet("Recommend")]
        public async Task<List<string>> GetRecommend()
        {
            try
            {
                var result = await sellOperation.GetAllRecommend();
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<string>();
            }

        }
        [Authorize]
        [HttpGet("AdminId-{id}")]
        public async Task<SearchDTO> GetAllId(int id)
        {
            try
            {
                var result = await sellOperation.GetAllId(id);
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
                var result = await sellOperation.GetAllOwnNumber(ONumber);
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
                var result = await sellOperation.GetAllCustomerNumber(CNumber);

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
                var result = await sellOperation.GetAllNormal(Page);
                return result.Data;
            }catch(Exception ex) { Console.WriteLine(ex.Message);return new SearchDTO(); }
            
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
                if (entity.Data == null)
                {
                    return;
                }

                try
                {
                    var imgIds = imgList.Data.Select(x => x.ImgPath).ToList();
                    var videoIds =entity.Data.VideoPath;
                   

                    DelResParams deleteParams = new DelResParams()
                    {
                        PublicIds = imgIds
                    };
                    if (videoIds!=null)
                    {
                        DelResResult resultVideo = cloudinary.DeleteResources(ResourceType.Video, videoIds);
                    }
                    

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
        [Authorize]
        [HttpPut("Recommend")]
        public async void UpdateRecommend([FromBody] Sell sell)
        {
            try
            {
                var items = await sellOperation.GetAllRecommend();
                if (items.Data.Count > 30)
                {
                    var lastItem = items.Data.LastOrDefault();
                    var Sell30 = JsonSerializer.Deserialize<Sell>(lastItem);
                    var LastId = await sellOperation.GetByIdListAdmin(Sell30.Id);
                    var LastIdString = JsonSerializer.Deserialize<Sell>(JsonSerializer.Serialize(LastId.Data));
                    LastIdString.Recommend = false;
                    sellOperation.Update(LastIdString);
                }
                sellOperation.Update(sell);

            }
            catch (Exception ex)
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
