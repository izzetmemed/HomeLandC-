using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObyektCustomerController : ControllerBase
    {
        public ObyektOperationCustomer RentCustomerProcess { get; set; }

        public ObyektCustomerController()
        {
            RentCustomerProcess = new ObyektOperationCustomer();
        }

        [HttpGet]
        public List<ObyektSecondStepCustomer> Get()
        {
            return RentCustomerProcess.GetAll().Data;
        }

        [HttpGet("{Id}")]
        public ObyektSecondStepCustomer Get(int Id)
        {
            return RentCustomerProcess.GetById(Id).Data;
        }

        [HttpPost]
        public void Add([FromBody] ObyektSecondStepCustomer entity)
        {
            RentCustomerProcess.Add(entity);
        }

        [HttpPut]
        public void Update([FromBody] ObyektSecondStepCustomer entity)
        {
            RentCustomerProcess.Update(entity);
        }

        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            var entity = RentCustomerProcess.GetById(Id).Data;
            RentCustomerProcess.Delete(entity);
        }
    }
}
