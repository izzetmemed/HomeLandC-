using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
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

        public ObyektCustomerController(ObyektOperationCustomer obyektOperationCustomer)
        {
            RentCustomerProcess = obyektOperationCustomer;
        }

        [Authorize]
        [HttpPost]
        public void Add([FromBody] ObyektSecondStepCustomer entity)
        {
            try
            {
                RentCustomerProcess.Add(entity);
            }catch (Exception ex) { Console.WriteLine(ex); }
           
        }
        [Authorize]
        [HttpPut]
        public void Update([FromBody] ObyektSecondStepCustomer entity)
        {
            try
            {
                RentCustomerProcess.Update(entity);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
           
        }
    }
}
