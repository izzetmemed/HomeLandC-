using Business.Abstract;
using Business.Message;
using CloudinaryDotNet.Actions;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Concrete;
using Model.DTOmodels;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentHomeOperation : IRentHomeService
    {
        Access Access;
        AccessImg AccessImg;
        AccessCustomer AccessCustomer;
        MediaOperation mediaOperation;
        public RentHomeOperation()
        {
            Access = new Access();
            AccessImg = new AccessImg();
            AccessCustomer = new AccessCustomer();
            mediaOperation = new MediaOperation();
        }
        public async Task<IResult> Add(RentHome Model)
        {
            string[] Check = { "Addition","CoordinateX", "CoordinateY" };

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

        public async Task<IDataResult<List<string>>> GetAll(Expression<Func<RentHome, bool>>? predicate = null)
        {
            return new SuccessDataResult<List<string>>( await Access.GetAll(predicate));
        }

        public async Task<IDataResult<List<string>>> GetAllCoordinate()
        {
            return new SuccessDataResult<List<string>>(await Access.GetAllCoordinate());
        }

        public async Task<IDataResult<SearchDTO>> GetAllCustomerNumber( string CNumber)
        {
            var result = await Access.GetAllNormal(1,x => x.SecondStepCustomers.Any(y => y.Number == CNumber));
            return new SuccessDataResult<SearchDTO>(result);
        }



        public async Task<IDataResult<SearchDTO>> GetAllId(int id)
        {
            return new SuccessDataResult<SearchDTO>(await Access.GetAllNormal(1,x=>x.Id==id));
        }

        public async Task<IDataResult<SearchDTO>> GetAllNormal( int Page)
        {
            return new SuccessDataResult<SearchDTO>(await Access.GetAllNormal(Page));
        }

        public async Task<IDataResult<SearchDTO>> GetAllOwnNumber( string ONumber)
        {
            var result = await Access.GetAllNormal(1,x => x.Number==ONumber);
            return new SuccessDataResult<SearchDTO>(result);
        }

        public async Task<IDataResult<SearchDTO>> GetAllPage(int Page)
        {
            return new SuccessDataResult<SearchDTO>(await Access.GetAllPage(Page));
        }

        public async Task<IDataResult<List<string>>> GetAllRecommend()
        {
            return new SuccessDataResult<List<string>>(await Access.GetAllRecommend());
        }

        public async Task<IDataResult<SearchDTO>> GetAllSearch(SearchModel searchModel, int Page)
        {
            return new SuccessDataResult<SearchDTO>(await Access.GetAllSearch(searchModel, Page));
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
            data.Looking = data.Looking + 1;
            await this.Update(data);
            var needData = new
            {
                Id = data.Id,
                Address = data.Address,
                Room = data.Room,
                Metro = data.Metro,
                Looking=data.Looking,
                Price = data.Price,
                Item = data.Item,
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
                Looking = data.Looking,
                Email =data.Email,
                Metro = data.Metro,
                Price = data.Price,
                Item = data.Item,
                Region = data.Region,
                Area = data.Area,
                Number=data.Number,
                Recommend = data.Recommend,
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
