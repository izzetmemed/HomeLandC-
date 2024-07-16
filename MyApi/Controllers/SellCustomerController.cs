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
        public SellCustomerController(SellOperationCustomer SellOperationCustomer)
        {
            sellOperationCustomer = SellOperationCustomer;
        }
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
    }
}
