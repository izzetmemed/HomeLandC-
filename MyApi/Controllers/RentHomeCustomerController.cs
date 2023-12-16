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

        [HttpGet]
        public List<SecondStepCustomer> Get()
        {
            return RentCustomerProcess.GetAll().Data;
        }

        [HttpGet("{Id}")]
        public SecondStepCustomer Get(int Id)
        {
            return RentCustomerProcess.GetById(Id).Data;
        }

        [HttpPost]
        public void Add([FromBody] SecondStepCustomer entity)
        {
            RentCustomerProcess.Add(entity);
        }

        [HttpPut]
        public void Update([FromBody] SecondStepCustomer entity)
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
