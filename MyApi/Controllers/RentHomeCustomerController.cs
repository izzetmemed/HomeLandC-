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
            var data = await RentCustomerProcess.GetById(Id);
            return data.Data;
        }

        [HttpPost]
        public void Add([FromBody] SecondStepCustomer entity)
        {
            RentCustomerProcess.Add(entity);
        }

        //[HttpPut]
        //public void Update([FromBody] SecondStepCustomer entity)
        //{
        //    RentCustomerProcess.Update(entity);
        //}

        //[HttpDelete("{Id}")]
        //public async void Delete(int Id)
        //{
        //    var entity =await RentCustomerProcess.GetById(Id);
        //    RentCustomerProcess.Delete(entity.Data);
        //}
    }
}
