using Business.Abstract;
using Business.Message;
using Core;
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
        public ObyektOperation()
        {
            obyektAccess = new ObyektAccess();
        }
        public IResult Add(Obyekt Model)
        {
            bool allPropertiesNullOrWhiteSpace = true;

            foreach (PropertyInfo property in typeof(Obyekt).GetProperties())
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
                obyektAccess.Add(Model);
                return new SuccessResult(MyMessage.Success);
            }
            else
            {
                return new ErrorResult("Some properties are not null or white space.");
            }
        }

        public IResult Delete(Obyekt Model)
        {
            obyektAccess.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<Obyekt>> GetAll()
        {
            return new SuccessDataResult<List<Obyekt>>(obyektAccess.GetAll());
        }

        public IDataResult<Obyekt> GetById(int id)
        {
            return new SuccessDataResult<Obyekt>(obyektAccess.GetById(x => x.Id == id));
        }

        public IResult Update(Obyekt Model)
        {
            obyektAccess.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
