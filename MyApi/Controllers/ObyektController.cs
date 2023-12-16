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
        public List<Obyekt> Get()
        {

            return RentProcess.GetAll().Data;
        }
        [HttpGet("{Id}")]
        public Obyekt Get(int Id)
        {
            return RentProcess.GetById(Id).Data;
        }
        [HttpPost]
        public void Add([FromBody] Obyekt entity)
        {
            RentProcess.Add(entity);
        }
        [HttpPut]
        public void Update([FromBody] Obyekt entity)
        {
            RentProcess.Update(entity);
        }
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            RentImgProcess.DeleteList(Id);
            RentCustomerProcess.DeleteList(Id);
            var entity = RentProcess.GetById(Id).Data;
            RentProcess.Delete(entity);
        }
    }
}
