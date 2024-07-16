using Business.Abstract;
using Business.Concrete;
using DataAccess.AccessingDbRent.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Collections.Generic;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentHomeCustomerController : ControllerBase
    {
        public RentHomeOperationCustomer RentCustomerProcess { get; set; }

        public RentHomeCustomerController(RentHomeOperationCustomer rentHomeOperationCustomer)
        {
            RentCustomerProcess = rentHomeOperationCustomer;
        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<SecondStepCustomer> Get(int Id)
        {
            try
            {
                var data = await RentCustomerProcess.GetById(Id);
                return data.Data;
            }catch (Exception ex) { 
                Console.WriteLine(ex); 
                return new SecondStepCustomer();
            }  
        }
        [Authorize]
        [HttpPost]
        public void Add([FromBody] SecondStepCustomer entity)
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
        public void Update([FromBody] SecondStepCustomer entity)
        {
            try
            {
                RentCustomerProcess.Update(entity);
            }catch(Exception ex) { Console.WriteLine(ex); }
           
        }
    }
}
