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
            var result = await sellOperation.GetAll();
            return result.Data;
        }
        [HttpGet("Normal")]
        public async Task<List<string>> GetNormal()
        {
            var result = await sellOperation.GetAllNormal();
            return result.Data;
        }
        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            var result = await sellOperation.GetByIdList(Id);
            return result.Data;
        }

        [HttpGet("Admin/{Id}")]
        public async Task<object> GetAdmin(int Id)
        {
            try
            {
                var result = await sellOperation.GetByIdListAdmin(Id);
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
           await sellOperation.Add(sell);
        }
        [HttpDelete("{Id}")]
        public async void Delete(int Id)
        {
            sellOperationImg.DeleteList(Id);
            sellOperationCustomer.DeleteList(Id);
            var entity = await sellOperation.GetById(Id);
            sellOperation.Delete(entity.Data);
        }
        [HttpPut]
        public void Update([FromBody] Sell sell)
        {
            sellOperation.Update(sell);
        }

    }
}
