using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SellOperationCustomer : ISellServiceCustomer
    {
       public SellAccessCustomer _accessCustomer;

        public SellOperationCustomer()
        {
            _accessCustomer = new SellAccessCustomer();
        }
        public IResult Add(SellSecondStepCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
                _accessCustomer.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IResult Delete(SellSecondStepCustomer Model)
        {
            _accessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<SellSecondStepCustomer>> GetAll()
        {
             return new SuccessDataResult<List<SellSecondStepCustomer>>( _accessCustomer.GetAll());

        }

        public IDataResult<SellSecondStepCustomer> GetById(int id)
        {
            return new SuccessDataResult<SellSecondStepCustomer>(_accessCustomer.GetById(x => x.SecondStepCustomerId == id));
        }

        public IResult Update(SellSecondStepCustomer Model)
        {
            _accessCustomer.Update(Model); 
            return new SuccessResult(MyMessage.Success);
        }
    }
}
