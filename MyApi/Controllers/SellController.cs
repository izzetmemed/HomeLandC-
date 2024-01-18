using _00.DataAccess.AccessingDbRent.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

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
        [HttpDelete("{Id}")]
        public async void Delete(int Id)
        {
            try
            {
                sellOperationImg.DeleteList(Id);
                sellOperationCustomer.DeleteList(Id);
                var entity = await sellOperation.GetById(Id);
                sellOperation.Delete(entity.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
           
        }
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

    }
}
