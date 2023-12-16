using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDb.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentHomeOperation : IRentHomeService
    {
        Access Access = new Access();
        public IResult Add(RentHome Model)
        {
            string[] Check = { "Addition", "CoordinateX", "CoordinateY" };

            bool allPropertiesNullOrWhiteSpace = true;

            foreach (PropertyInfo property in typeof(RentHome).GetProperties())
            {
               
                if (property.PropertyType == typeof(string))
                {   
                    string? value = (string?)property.GetValue(Model);
                    if(Check.Contains(property.Name))
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
                return new SuccessResult(MyMessage.Success);
            }
            else
            {
                return new ErrorResult("Some properties are not null or white space.");
            }
         }



        public IResult Delete(RentHome Model)
        {
            Access.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<RentHome>> GetAll()
        {
            return new SuccessDataResult<List<RentHome>>(Access.GetAll());
        }
    public IDataResult<RentHome> GetById(int id)
        {
            return new SuccessDataResult<RentHome>(Access.GetById(x => x.Id == id));
        }

        public IResult Update(RentHome Model)
        {
            Access.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
