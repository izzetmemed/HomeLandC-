using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

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
        [HttpGet("Admin/{Id}")]
        public async Task<object> GetAdmin(int Id)
        {
            try
            {
                var result = await RentProcess.GetByIdListAdmin(Id);
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
        [HttpPut]
        public async void Update( Obyekt entity)
        {
            try
            {
                await RentProcess.Update(entity);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
          
        }
        [HttpDelete("{Id}")]
        public async void Delete(int Id)
        {
            try
            {
                RentImgProcess.DeleteList(Id);
                await RentCustomerProcess.DeleteList(Id);
                var entity = await RentProcess.GetById(Id);
                RentProcess.Delete(entity.Data);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
           
        }
    }
}
