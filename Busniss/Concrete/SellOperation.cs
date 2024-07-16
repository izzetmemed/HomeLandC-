using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Concrete;
using Model.DTOmodels;
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
    public class SellOperation : ISellService
    {
        SellAccess sellAccess;
        SellAccessImg sellAccessImg;
        SellAccessCustomer sellAccessCustomer ;
        MediaOperation mediaOperation;
        public SellOperation()
        {
            sellAccess = new SellAccess();
            sellAccessImg=new SellAccessImg();
            sellAccessCustomer=new SellAccessCustomer();
            mediaOperation = new MediaOperation();
        }
        public async Task<IResult> Add(Sell Model)
        {
            string[] Check = { "Addition", "CoordinateX", "CoordinateY", "VideoPath" };

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
                mediaOperation.MakeContactSell(Model);
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
        public async Task<IDataResult<List<string>>> GetAll(Expression<Func<Sell, bool>>? predicate = null)
        {
            return new SuccessDataResult<List<string>>(await sellAccess.GetAll(predicate));
        }
        public async Task<IDataResult<List<string>>> GetAllCoordinate()
        {
            return new SuccessDataResult<List<string>>(await sellAccess.GetAllCoordinate());
        }

        public async Task<IDataResult<SearchDTO>> GetAllCustomerNumber(string CNumber)
        {
            var result = await sellAccess.GetAllNormal(1, x => x.SellSecondStepCustomers.Any(y => y.Number == CNumber));
            return new SuccessDataResult<SearchDTO>(result);
        }

        public async Task<IDataResult<SearchDTO>> GetAllId(int id)
        {
            return new SuccessDataResult<SearchDTO>(await sellAccess.GetAllNormal(1, x => x.Id == id));
        }

        public async Task<IDataResult<SearchDTO>> GetAllNormal(int Page)
        {
            return new SuccessDataResult<SearchDTO>(await sellAccess.GetAllNormal(Page));
        }

        public async Task<IDataResult<SearchDTO>> GetAllOwnNumber(string ONumber)
        {
            var result = await sellAccess.GetAllNormal(1, x => x.Number == ONumber);
            return new SuccessDataResult<SearchDTO>(result);
        }

        public async Task<IDataResult<SearchDTO>> GetAllPage(int Page)
        {
            return new SuccessDataResult<SearchDTO>(await sellAccess.GetAllPage(Page));
        }
        public async Task<IDataResult<List<string>>> GetAllRecommend()
        {
            return new SuccessDataResult<List<string>>(await sellAccess.GetAllRecommend());
        }

        public async Task<IDataResult<SearchDTO>> GetAllSearch(SearchModel searchModel, int Page)
        {
            return new SuccessDataResult<SearchDTO>(await sellAccess.GetAllSearch(searchModel, Page));
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
            data.Looking = data.Looking + 1;
            await this.Update(data);
            var needData = new
            {
                Id = data.Id,
                Address = data.Address,
                Room = data.Room,
                Looking = data.Looking,
                Metro = data.Metro,
                Price = data.Price,
                Item = data.Item,
                Region = data.Region,
                Area = data.Area,
                Document = data.Document,
                Date = data.Date,
                Fullname = data.Fullname,
                Floor = data.Floor,
                CoordinateX = data.CoordinateX,
                CoordinateY = data.CoordinateY,
                Repair = data.Repair,
                VideoPath=data.VideoPath,
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
                Looking = data.Looking,
                Email = data.Email,
                Item = data.Item,
                Region = data.Region,
                Area = data.Area,
                Document=data.Document,
                Number=data.Number,
                Recommend=data.Recommend,
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
