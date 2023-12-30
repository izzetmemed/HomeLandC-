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
        //public ObyektOperationCustomer RentCustomerProcess { get; set; }
        public ObyektOperation RentProcess { get; set; }
        public ObyektOperationImg RentImgProcess { get; set; }

        public ObyektController()
        {
            //RentCustomerProcess = new ObyektOperationCustomer();
            RentProcess = new ObyektOperation();
            RentImgProcess = new ObyektOperationImg();
        }
        [HttpGet]
        public async Task<List<string>> Get()
        {
            var result = await RentProcess.GetAll();
            return result.Data;
        }
        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            var result = await RentProcess.GetByIdList(Id);
            return result.Data;
        }
        [HttpPost]
        public async void Add( Obyekt entity)
        {
           await RentProcess.Add(entity);
        }
        [HttpPut]
        public async void Update( Obyekt entity)
        {
           await RentProcess.Update(entity);
        }
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            //RentImgProcess.DeleteList(Id);
            //RentCustomerProcess.DeleteList(Id);
            //var entity = RentProcess.GetById(Id).Data;
            //RentProcess.Delete(entity);
        }
    }
}
