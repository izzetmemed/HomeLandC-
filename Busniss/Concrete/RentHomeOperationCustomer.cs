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
        public async Task<IResult> Add(SecondStepCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
            AccessCustomer.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> Delete(SecondStepCustomer Model)
        {
            AccessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await AccessCustomer.GetAll());
        }

        public async Task<IDataResult<SecondStepCustomer>> GetById(int id)
        {
            return new SuccessDataResult<SecondStepCustomer>(AccessCustomer.GetById(x => x.SecondStepCustomerId == id));
        }

        public async Task<IDataResult<List<SecondStepCustomer>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<SecondStepCustomer>>(await AccessCustomer.GetByIdList(id));
        }
        public async Task<IResult> Update(SecondStepCustomer Model)
        {
            AccessCustomer.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public async Task<IResult> DeleteList(int ForeignId)
        {
            AccessCustomer.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }

    }
}
