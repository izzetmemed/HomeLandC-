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
        public IResult Add(Sell Model)
        {
            bool allPropertiesNullOrWhiteSpace = true;

            foreach (PropertyInfo property in typeof(Sell).GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    string? value = (string?)property.GetValue(Model);
                    if (string.IsNullOrWhiteSpace(value))
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
                sellAccess.Add(Model);
                return new SuccessResult(MyMessage.Success);
            }
            else
            {
                return new ErrorResult("Some properties are not null or white space.");
            }
        }

        public IResult Delete(Sell Model)
        {
            sellAccess.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<Sell>> GetAll()
        {
           return new SuccessDataResult<List<Sell>>(sellAccess.GetAll());
        }

        public IDataResult<Sell> GetById(int id)
        {
            return new SuccessDataResult<Sell>(sellAccess.GetById(x=>x.Id==id));
        }

        public IResult Update(Sell Model)
        {
            sellAccess.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
