using Business.Abstract;
using Business.Message;
using CloudinaryDotNet.Actions;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentHomeOperation : IRentHomeService
    {
        Access Access = new Access();
        AccessImg AccessImg = new AccessImg();
        AccessCustomer AccessCustomer = new AccessCustomer();
        MediaOperation mediaOperation= new MediaOperation();
        public async Task<IResult> Add(RentHome Model)
        {
            string[] Check = { "Addition", "CoordinateX", "CoordinateY" };

            bool allPropertiesNullOrWhiteSpace = true;

            foreach (PropertyInfo property in typeof(RentHome).GetProperties())
            {

                if (property.PropertyType == typeof(string))
                {
                    string? value = (string?)property.GetValue(Model);
                    if (Check.Contains(property.Name))
                    {

                    }
                    else if (string.IsNullOrWhiteSpace(value))
                    {
                        allPropertiesNullOrWhiteSpace = false;
                        break;
                    }
                }
                else if (property.PropertyType == typeof(int))
                {
                    int? value = (int?)property.GetValue(Model);
                    if (value == null)
                    {
                        allPropertiesNullOrWhiteSpace = false;
                        break;
                    }
                }
            }

            if (allPropertiesNullOrWhiteSpace)
            {
                Access.Add(Model);
                mediaOperation.MakeContact(Model);
                return new SuccessResult(MyMessage.Success);
            }
            else
            {
                return new ErrorResult("Some properties are not null or white space.");
            }
        }



        public async Task<IResult> Delete(RentHome Model)
        {
            Access.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>( await Access.GetAll());
        }

        public async Task<IDataResult<List<string>>> GetAllCoordinate()
        {
            return new SuccessDataResult<List<string>>(await Access.GetAllCoordinate());
        }

        public async Task<IDataResult<List<string>>> GetAllNormal()
        {
            return new SuccessDataResult<List<string>>(await Access.GetAllNormal());
        }

        public async Task<IDataResult<RentHome>> GetById(int id)
        {
            return new SuccessDataResult<RentHome>(Access.GetById(x => x.Id == id));
        }

        public async Task<IDataResult<object>> GetByIdList(int id)
        {
            var data = Access.GetById(x => x.Id == id);
            var img =await AccessImg.GetByIdList(id);
            if (data == null)
            {
                return null;
            }
            var needData = new
            {
                Id = data.Id,
                Address = data.Address,
                Room = data.Room,
                Metro = data.Metro,
                Price = data.Price,
                Item = data.İtem,
                Region = data.Region,
                Area = data.Area,
                Date = data.Date,
                Floor=data.Floor,
                CoordinateX=data.CoordinateX,
                CoordinateY=data.CoordinateY,
                Repair=data.Repair,
                Building=data.Building,
                Bed=data.Bed,
                Wardrobe=data.Wardrobe,
                TableChair=data.TableChair,
                CentralHeating=data.CentralHeating,
                GasHeating=data.GasHeating,
                Combi=data.Combi,
                Tv=data.Tv,
                WashingClothes=data.WashingClothes,
                AirConditioning=data.AirConditioning,
                Sofa=data.Sofa,
                Wifi=data.Wifi,
                Addition=data.Addition,
                Boy=data.Boy,
                Girl=data.Girl,
                WorkingBoy=data.WorkingBoy,
                Family=data.Family,
                Img = img.Select(x => x.ImgPath).ToList()
            };

            return new SuccessDataResult<object>(needData);
        }

        public async Task<IDataResult<object>> GetByIdListAdmin(int id)
        {
            var data = Access.GetById(x => x.Id == id);
            var img = await AccessImg.GetByIdList(id);
             var customer= await AccessCustomer.GetByIdList(id);
            if (data == null)
            {
                return null;
            }
            var needData = new
            {
                Id = data.Id,
                Address = data.Address,
                Room = data.Room,
                Metro = data.Metro,
                Price = data.Price,
                İtem = data.İtem,
                Region = data.Region,
                Area = data.Area,
                Number=data.Number,
                Date = data.Date,
                Fullname = data.Fullname,
                Floor = data.Floor,
                CoordinateX = data.CoordinateX,
                CoordinateY = data.CoordinateY,
                Repair = data.Repair,
                Building = data.Building,
                Bed = data.Bed,
                Wardrobe = data.Wardrobe,
                TableChair = data.TableChair,
                CentralHeating = data.CentralHeating,
                GasHeating = data.GasHeating,
                Combi = data.Combi,
                Tv = data.Tv,
                WashingClothes = data.WashingClothes,
                AirConditioning = data.AirConditioning,
                Sofa = data.Sofa,
                Wifi = data.Wifi,
                Addition = data.Addition,
                Boy = data.Boy,
                Girl = data.Girl,
                WorkingBoy = data.WorkingBoy,
                Family = data.Family,
                IsCalledWithHomeOwnFirstStep = data.IsCalledWithHomeOwnFirstStep,
                IsCalledWithCustomerFirstStep = data.IsCalledWithCustomerFirstStep,
                IsPaidHomeOwnFirstStep = data.IsPaidHomeOwnFirstStep,
                IsPaidCustomerFirstStep = data.IsPaidCustomerFirstStep,
                IsCalledWithHomeOwnThirdStep = data.IsCalledWithHomeOwnThirdStep,
                Img = img.Select(x => x.ImgPath).ToList(),
                Customer= customer.ToList(),
            };

            return new SuccessDataResult<object>(needData);
        }

        public async Task<IResult> Update(RentHome Model)
        {
            Access.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }

       
    }
}
