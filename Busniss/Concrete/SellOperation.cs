using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SellOperation : ISellService
    {
        SellAccess sellAccess = new SellAccess();
        SellAccessImg sellAccessImg=new SellAccessImg();
        SellAccessCustomer sellAccessCustomer = new SellAccessCustomer();
        public async Task<IResult> Add(Sell Model)
        {
            string[] Check = { "Addition", "CoordinateX", "CoordinateY" };

            bool allPropertiesNullOrWhiteSpace = true;

            foreach (PropertyInfo property in typeof(Sell).GetProperties())
            {

                if (property.PropertyType == typeof(string))
                {
                    string? value = (string?)property.GetValue(Model);
                    if (Check.Contains(property.Name))
                    {

                    }
                    else if (string.IsNullOrWhiteSpace(value))
                    {
                        var name = property.Name;
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
                sellAccess.Add(Model);
                return new SuccessResult(MyMessage.Success);
            }
            else
            {
                return new ErrorResult("Some properties are not null or white space.");
            }
        }

        public async Task<IResult> Delete(Sell Model)
        {
            sellAccess.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await sellAccess.GetAll());
        }

        public async Task<IDataResult<List<string>>> GetAllNormal()
        {
            return new SuccessDataResult<List<string>>(await sellAccess.GetAllNormal());
        }

        public async Task<IDataResult<Sell>> GetById(int id)
        {
            return new SuccessDataResult<Sell>(sellAccess.GetById(x => x.Id == id));
        }

        public async Task<IDataResult<object>> GetByIdList(int id)
        {
            var data = sellAccess.GetById(x => x.Id == id);
            var img = await sellAccessImg.GetByIdList(id);
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
                Document = data.Document,
                Date = data.Date,
                Fullname = data.Fullname,
                Floor = data.Floor,
                CoordinateX = data.CoordinateX,
                CoordinateY = data.CoordinateY,
                Repair = data.Repair,
                Building = data.Building,
                Addition = data.Addition,
                Img = img.Select(x => x.ImgPath).ToList()
            };

            return new SuccessDataResult<object>(needData);
        }

        public async Task<IDataResult<object>> GetByIdListAdmin(int id)
        {
             var data = sellAccess.GetById(x => x.Id == id);
            var img = await sellAccessImg.GetByIdList(id);
             var customer= await sellAccessCustomer.GetByIdList(id);
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
                Document=data.Document,
                Number=data.Number,
                Date = data.Date,
                Fullname = data.Fullname,
                Floor = data.Floor,
                CoordinateX = data.CoordinateX,
                CoordinateY = data.CoordinateY,
                Repair = data.Repair,
                Building = data.Building,
                Addition = data.Addition,
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

        public async Task<IResult> Update(Sell Model)
        {
            sellAccess.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
