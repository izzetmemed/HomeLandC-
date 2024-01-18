using Business.Abstract;
using Business.Concrete;
using DataAccess.AccessingDbRent.Concrete;
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

        public RentHomeCustomerController()
        {
            RentCustomerProcess = new RentHomeOperationCustomer();
        }

        //[HttpGet]
        //public async Task<List<string>> Get()
        //{
        //    var data =await RentCustomerProcess.GetAll();
        //    return data.Data;
        //}

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

        [HttpPut]
        public void Update([FromBody] SecondStepCustomer entity)
        {
            try
            {
                RentCustomerProcess.Update(entity);
            }catch(Exception ex) { Console.WriteLine(ex); }
           
        }

        //[HttpDelete("{Id}")]
        //public async void Delete(int Id)
        //{
        //    var entity =await RentCustomerProcess.GetById(Id);
        //    RentCustomerProcess.Delete(entity.Data);
        //}
    }
}
