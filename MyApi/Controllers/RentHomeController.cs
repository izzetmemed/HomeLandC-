using _00.DataAccess.AccessingDb.Abstract;
using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class RentHomeController : ControllerBase
    {
        public RentHomeOperationCustomer RentCustomerProcess { get; set; }
        public IRentHomeService RentProcess { get; set; }
        public RentHomeOperationImg RentImgProcess { get; set; }

        public RentHomeController()
        {
            RentCustomerProcess = new RentHomeOperationCustomer();
            RentProcess = new RentHomeOperation();
            RentImgProcess = new RentHomeOperationImg();
        }
        [HttpGet]
        public List<RentHome> Get()
        {

            return RentProcess.GetAll().Data;
        }
        [HttpGet("{Id}")]
        public RentHome Get(int Id)
        {
            return RentProcess.GetById(Id).Data;
        }
        [HttpPost]
        public void Add(RentHome rentHomeObject)
        {
           
                //var Entity = new RentHome()
                //{
                //    Floor = rentHomeObject.Floor,
                //    Fullname = rentHomeObject.Fullname,
                //    Addition = rentHomeObject.Addition,
                //    AirConditioning = rentHomeObject.AirConditioning,
                //    Area = rentHomeObject.Area,
                //    Building = rentHomeObject.Building,
                //    Bed = rentHomeObject.Bed,
                //    Combi = rentHomeObject.Combi,
                //    CentralHeating = rentHomeObject.CentralHeating,
                //    GasHeating = rentHomeObject.GasHeating,
                //    CoordinateX = rentHomeObject.CoordinateX,
                //    CoordinateY = rentHomeObject.CoordinateY,
                //    ImgNames = rentHomeObject.ImgNames,
                //    Date = rentHomeObject.Date,
                //    Address = rentHomeObject.Address,
                //    IsCalledWithCustomerFirstStep = rentHomeObject.IsCalledWithCustomerFirstStep,
                //    IsCalledWithHomeOwnFirstStep = rentHomeObject.IsCalledWithHomeOwnFirstStep,
                //    IsCalledWithHomeOwnThirdStep = rentHomeObject.IsCalledWithHomeOwnThirdStep,
                //    IsPaidCustomerFirstStep = rentHomeObject.IsCalledWithCustomerFirstStep,
                //    IsPaidHomeOwnFirstStep = rentHomeObject.IsPaidHomeOwnFirstStep,
                //    Metro = rentHomeObject.Metro,
                //    Number = rentHomeObject.Number,
                //    Price = rentHomeObject.Price,
                //    Region = rentHomeObject.Region,
                //    Repair = rentHomeObject.Repair,
                //    Room = rentHomeObject.Room,
                //    SecondStepCustomers = rentHomeObject.SecondStepCustomers,
                //    Sofa = rentHomeObject.Sofa,
                //    TableChair = rentHomeObject.TableChair,
                //    Tv = rentHomeObject.Tv,
                //    Wardrobe = rentHomeObject.Wardrobe,
                //    WashingClothes = rentHomeObject.WashingClothes,
                //    Wifi = rentHomeObject.Wifi,
                //    İtem = rentHomeObject.İtem,
                //    Id = rentHomeObject.Id
                //};

                RentProcess.Add(rentHomeObject);
        }
        [HttpPut]
        public void Update([FromBody] RentHome entity)
        {
            RentProcess.Update(entity);
        }
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            RentImgProcess.DeleteList(Id);
            RentCustomerProcess.DeleteList(Id);
            var entity = RentProcess.GetById(Id).Data;
            RentProcess.Delete(entity);
        }
    }
}
