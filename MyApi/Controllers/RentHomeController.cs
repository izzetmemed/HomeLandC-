using _00.DataAccess.AccessingDb.Abstract;
using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MyApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class RentHomeController : ControllerBase
    {
        public RentHomeOperationCustomer RentCustomerProcess { get; set; }
        public RentHomeOperation RentProcess { get; set; }
        public RentHomeOperationImg RentImgProcess { get; set; }

        public RentHomeController()
        {
            RentCustomerProcess = new RentHomeOperationCustomer();
            RentProcess = new RentHomeOperation();
            RentImgProcess = new RentHomeOperationImg();
        }
        [HttpGet]
        public async Task<List<string>> Get()
        {
            var result = await RentProcess.GetAll();
            return result.Data;
        }
        [HttpGet("Normal")]
        public async Task<List<string>> GetNormal()
        {
            var result = await RentProcess.GetAllNormal();
            return result.Data;
        }


        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            var result = await RentProcess.GetByIdList(Id);
            return result.Data;
        }
        [HttpGet("Admin/{Id}")]
        public async Task<object> GetAdmin(int Id)
        {
            try
            {
                var result = await RentProcess.GetByIdListAdmin(Id);
                return result.Data;
            }catch(Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
           
        }

        [HttpPost]
        public async void Add(RentHome rentHomeObject)
        {
            await RentProcess.Add(rentHomeObject);
        }
        [HttpPut]
        public async void Update([FromBody] RentHome entity)
        {
            await RentProcess.Update(entity);
        }
        [HttpDelete("{Id}")]
        public async void Delete(int Id)
        {
            RentImgProcess.DeleteList(Id);
            RentCustomerProcess.DeleteList(Id);
            var entity = await RentProcess.GetById(Id);
            RentProcess.Delete(entity.Data);
        }
    }
}
