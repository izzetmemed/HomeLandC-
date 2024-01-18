using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete;
using DataAccess.Repository;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ObyektOperationCustomer : IObyektServiceCustomer
    {
        public ObyectAccessCustomer obyectAccessCustomer { get; set; }

        public ObyektOperationCustomer()
        {
            obyectAccessCustomer = new ObyectAccessCustomer();
        }
        public async Task<IResult> Add(ObyektSecondStepCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
            obyectAccessCustomer.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> Delete(ObyektSecondStepCustomer Model)
        {
            obyectAccessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await obyectAccessCustomer.GetAll());
        }

        public async Task<IDataResult<ObyektSecondStepCustomer>> GetById(int id)
        {
            return new SuccessDataResult<ObyektSecondStepCustomer>(obyectAccessCustomer.GetById(x => x.SecondStepCustomerId == id));
        }
        public async Task<IDataResult<List<ObyektSecondStepCustomer>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<ObyektSecondStepCustomer>>(await obyectAccessCustomer.GetByIdList(id));
        }
        public async Task<IResult> Update(ObyektSecondStepCustomer Model)
        {
            obyectAccessCustomer.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public async Task<IResult> DeleteList(int ForeignId)
        {
            obyectAccessCustomer.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
