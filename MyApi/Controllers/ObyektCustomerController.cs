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

        //[HttpGet]
        //public List<ObyektSecondStepCustomer> Get()
        //{
        //    return RentCustomerProcess.GetAll().Data;
        //}

        //[HttpGet("{Id}")]
        //public ObyektSecondStepCustomer Get(int Id)
        //{
        //    return RentCustomerProcess.GetById(Id).Data;
        //}

        [HttpPost]
        public void Add([FromBody] ObyektSecondStepCustomer entity)
        {
            try
            {
                RentCustomerProcess.Add(entity);
            }catch (Exception ex) { Console.WriteLine(ex); }
           
        }

        [HttpPut]
        public void Update([FromBody] ObyektSecondStepCustomer entity)
        {
            try
            {
                RentCustomerProcess.Update(entity);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
           
        }

        //[HttpDelete("{Id}")]
        //public void Delete(int Id)
        //{
        //    var entity = RentCustomerProcess.GetById(Id).Data;
        //    RentCustomerProcess.Delete(entity);
        //}
    }
}
