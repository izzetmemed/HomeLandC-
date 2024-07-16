using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeCustomerController : ControllerBase
    {
        public OfficeCustomerOperation RentCustomerProcess { get; set; }

        public OfficeCustomerController(OfficeCustomerOperation officeCustomerOperation)
        {
            RentCustomerProcess = officeCustomerOperation;
        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<OfficeCustomer> Get(int Id)
        {
            try
            {
                var data = await RentCustomerProcess.GetById(Id);
                return data.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new OfficeCustomer();
            }
        }
        [Authorize]
        [HttpPost]
        public void Add([FromBody] OfficeCustomer entity)
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
        public void Update([FromBody] OfficeCustomer entity)
        {
            try
            {
                RentCustomerProcess.Update(entity);
            }
            catch (Exception ex) { Console.WriteLine(ex); }

        }
    }
}
