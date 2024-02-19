using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellCustomerController : ControllerBase
    {
        public SellOperationCustomer sellOperationCustomer { get; set; }
        public SellCustomerController()
        {
            sellOperationCustomer = new SellOperationCustomer();
        }
        //[HttpGet]
        //public List<SellSecondStepCustomer> Get()
        //{

        //    return sellOperationCustomer.GetAll().Data;
        //}
        //[HttpGet("{Id}")]
        //public SellSecondStepCustomer Get(int Id)
        //{
        //    return sellOperationCustomer.GetById(Id).Data;
        //}
        [Authorize]
        [HttpPost]
        public void Add([FromBody] SellSecondStepCustomer entity)
        {
            try
            {
                sellOperationCustomer.Add(entity);
            }catch (Exception ex) { Console.WriteLine(ex); }
           
        }
        [Authorize]
        [HttpPut]
        public void Update([FromBody] SellSecondStepCustomer entity)
        {
            try
            {
                sellOperationCustomer.Update(entity);
            }catch(Exception ex) { Console.WriteLine(ex); }
           
        }
        //[HttpDelete("{Id}")]
        //public void Delete(int Id)
        //{
        //    var entity = sellOperationCustomer.GetById(Id).Data;
        //    sellOperationCustomer.Delete(entity);
        //}

    }
}
