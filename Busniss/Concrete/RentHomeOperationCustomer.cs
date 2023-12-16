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
    public class RentHomeOperationCustomer : IRentHomeServiceCustomer
    {
        AccessCustomer AccessCustomer = new AccessCustomer();
        public IResult Add(SecondStepCustomer Model)
        {
            if(string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
                {
                    return new ErrorResult(MyMessage.Error);
                }
            AccessCustomer.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IResult Delete(SecondStepCustomer Model)
        {
            AccessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<SecondStepCustomer>> GetAll()
        {
            return new SuccessDataResult<List<SecondStepCustomer>>(AccessCustomer.GetAll());
        }

        public IDataResult<SecondStepCustomer> GetById(int id)
        {
            return new SuccessDataResult<SecondStepCustomer>(AccessCustomer.GetById(x => x.SecondStepCustomerId == id));
        }

        public IResult Update(SecondStepCustomer Model)
        {
            AccessCustomer.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public IResult DeleteList(int ForeignId)
        {
            AccessCustomer.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }

    }
}
