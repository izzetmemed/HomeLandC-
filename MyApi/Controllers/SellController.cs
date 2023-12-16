using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellController : ControllerBase
    {
        public SellOperation sellOperation { get; set; }
        public SellController()
        {
            sellOperation = new SellOperation();
        }
        [HttpGet]
        public List<Sell> Get()
        {
            return sellOperation.GetAll().Data;
        }
        [HttpGet("{Id}")]
        public Sell Get(int Id)
        {
            return sellOperation.GetById(Id).Data;
        }
        [HttpPost]
        public void Add([FromBody] Sell sell)
        {
            sellOperation.Add(sell);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            //?
            var model = sellOperation.GetById(id).Data;
            sellOperation.Delete(model);
        }
        [HttpPut]
        public void Update([FromBody] Sell sell)
        {
            sellOperation.Update(sell);
        }

    }
}
