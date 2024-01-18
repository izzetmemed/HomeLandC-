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
        public async Task<IResult> Add(SellSecondStepCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
            _accessCustomer.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> Delete(SellSecondStepCustomer Model)
        {
            _accessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await _accessCustomer.GetAll());
        }

        public async Task<IDataResult<SellSecondStepCustomer>> GetById(int id)
        {
            return new SuccessDataResult<SellSecondStepCustomer>(_accessCustomer.GetById(x => x.SecondStepCustomerId == id));

        }
        public async Task<IDataResult<List<SellSecondStepCustomer>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<SellSecondStepCustomer>>(await _accessCustomer.GetByIdList(id));
        }
        public async Task<IResult> Update(SellSecondStepCustomer Model)
        {
            _accessCustomer.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public async Task<IResult> DeleteList(int ForeignId)
        {
            _accessCustomer.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }
    }
} 
