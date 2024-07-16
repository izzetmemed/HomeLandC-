﻿using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Abstract;
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

        public async Task<IDataResult<List<string>>> GetAll(Expression<Func<Obyekt, bool>>? predicate = null)
        {
            return new SuccessDataResult<List<string>>(await obyektAccess.GetAll(predicate));
        }

        public async Task<IDataResult<List<string>>> GetAllCoordinate()
        {
            return new SuccessDataResult<List<string>>(await obyektAccess.GetAllCoordinate());
        }

        public async Task<IDataResult<SearchDTO>> GetAllCustomerNumber(string CNumber)
        {
            var result = await obyektAccess.GetAllNormal(1, x => x.ObyektSecondStepCustomers.Any(y => y.Number == CNumber));
            return new SuccessDataResult<SearchDTO>(result);
        }

        public async Task<IDataResult<SearchDTO>> GetAllId(int id)
        {
            return new SuccessDataResult<SearchDTO>(await obyektAccess.GetAllNormal(1, x => x.Id == id));
        }

        public async Task<IDataResult<SearchDTO>> GetAllNormal(int Page)
        {
            return new SuccessDataResult<SearchDTO>(await obyektAccess.GetAllNormal(Page));
        }

        public async Task<IDataResult<SearchDTO>> GetAllOwnNumber(string ONumber)
        {
            var result = await obyektAccess.GetAllNormal(1, x => x.Number == ONumber);
            return new SuccessDataResult<SearchDTO>(result);
        }

        public async Task<IDataResult<SearchDTO>> GetAllPage(int Page)
        {
            return new SuccessDataResult<SearchDTO>(await obyektAccess.GetAllPage(Page));
        }

        public async Task<IDataResult<List<string>>> GetAllRecommend()
        {
            return new SuccessDataResult<List<string>>(await obyektAccess.GetAllRecommend());
        }

        public async Task<IDataResult<SearchDTO>> GetAllSearch(SearchModel searchModel, int Page)
        {
            return new SuccessDataResult<SearchDTO>(await obyektAccess.GetAllSearch(searchModel, Page));
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
            data.Looking = data.Looking + 1;
            await this.Update(data);
            var needData = new
            {
                Id = data.Id,
                Address = data.Address,
                Room = data.Room,
                Metro = data.Metro,
                Looking = data.Looking,
                Price = data.Price,
                Item = data.Item,
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
                Looking = data.Looking,
                Email = data.Email,
                Price = data.Price,
                Item = data.Item,
                Region = data.Region,
                Area = data.Area,
                Number = data.Number,
                Date = data.Date,
                Fullname = data.Fullname,
                Recommend = data.Recommend,
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
