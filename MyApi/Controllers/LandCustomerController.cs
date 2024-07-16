using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandCustomerController : ControllerBase
    {
        public LandCustomerOperation RentCustomerProcess { get; set; }

        public LandCustomerController(LandCustomerOperation landCustomerOperation)
        {
            RentCustomerProcess = landCustomerOperation;
        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<LandCustomer> Get(int Id)
        {
            try
            {
                var data = await RentCustomerProcess.GetById(Id);
                return data.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new LandCustomer();
            }
        }
        [Authorize]
        [HttpPost]
        public void Add([FromBody] LandCustomer entity)
        {
            try
            {
                RentCustomerProcess.Add(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        [Authorize]
        [HttpPut]
        public void Update([FromBody] LandCustomer entity)
        {
            try
            {
                RentCustomerProcess.Update(entity);
            }
            catch (Exception ex) { Console.WriteLine(ex); }

        }
    }
}
