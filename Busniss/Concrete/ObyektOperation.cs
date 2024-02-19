using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ObyektOperation : IObyektService
    {
        public ObyektAccess obyektAccess;
        public ObyektAccessImg obyektAccessImg;
        public ObyectAccessCustomer obyectAccessCustomer;
        public MediaOperation mediaOperation;
        public ObyektOperation()
        {
            obyektAccess = new ObyektAccess();
            obyektAccessImg = new ObyektAccessImg();
            obyectAccessCustomer=new ObyectAccessCustomer();
            mediaOperation = new MediaOperation();
        }
        public async Task<IResult> Add(Obyekt Model)
        {
            string[] Check = { "Addition", "CoordinateX", "CoordinateY", "Document" };

            bool allPropertiesNullOrWhiteSpace = true;

            foreach (PropertyInfo property in typeof(Obyekt).GetProperties())
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
                obyektAccess.Add(Model);
                mediaOperation.MakeContactObyekt(Model);
                return new SuccessResult(MyMessage.Success);
            }
            else
            {
                return new ErrorResult("Some properties are not null or white space.");
            }
        }

        public async Task<IResult> Delete(Obyekt Model)
        {
            obyektAccess.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await obyektAccess.GetAll());
        }

        public async Task<IDataResult<List<string>>> GetAllCoordinate()
        {
            return new SuccessDataResult<List<string>>(await obyektAccess.GetAllCoordinate());
        }

        public async Task<IDataResult<List<string>>> GetAllNormal()
        {
            return new SuccessDataResult<List<string>>(await obyektAccess.GetAllNormal());
        }

        public async Task<IDataResult<Obyekt>> GetById(int id)
        {
            return new SuccessDataResult<Obyekt>(obyektAccess.GetById(x => x.Id == id));
        }

        public async Task<IDataResult<object>> GetByIdList(int id)
        {
            var data = obyektAccess.GetById(x => x.Id == id);
            var img = await obyektAccessImg.GetByIdList(id);
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
                CoordinateX = data.CoordinateX,
                CoordinateY = data.CoordinateY,
                Repair = data.Repair,
                Addition = data.Addition,
                Document = data.Document,
                SellorRent = data.SellOrRent,
                Img = img.Select(x => x.ImgPath).ToList()
            };

            return new SuccessDataResult<object>(needData);
        }

        public async Task<IDataResult<object>> GetByIdListAdmin(int id)
        {
            var data = obyektAccess.GetById(x => x.Id == id);
            var img = await obyektAccessImg.GetByIdList(id);
            var customer = await obyectAccessCustomer.GetByIdList(id);
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
                Number = data.Number,
                Date = data.Date,
                Fullname = data.Fullname,
                CoordinateX = data.CoordinateX,
                CoordinateY = data.CoordinateY,
                Repair = data.Repair,
                Addition = data.Addition,
                Document = data.Document,
                SellorRent = data.SellOrRent,
                IsCalledWithHomeOwnFirstStep = data.IsCalledWithHomeOwnFirstStep,
                IsCalledWithCustomerFirstStep = data.IsCalledWithCustomerFirstStep,
                IsPaidHomeOwnFirstStep = data.IsPaidHomeOwnFirstStep,
                IsPaidCustomerFirstStep = data.IsPaidCustomerFirstStep,
                IsCalledWithHomeOwnThirdStep = data.IsCalledWithHomeOwnThirdStep,
                Img = img.Select(x => x.ImgPath).ToList(),
                Customer = customer.ToList(),
            };

            return new SuccessDataResult<object>(needData);
        }

        public async Task<IResult> Update(Obyekt Model)
        {
            obyektAccess.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
