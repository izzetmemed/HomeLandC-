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
        public  ObyectAccessCustomer obyectAccessCustomer { get; set; }

        public ObyektOperationCustomer()
        {
            obyectAccessCustomer = new ObyectAccessCustomer();
        }
        public IResult Add(ObyektSecondStepCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
            obyectAccessCustomer.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IResult Delete(ObyektSecondStepCustomer Model)
        {
            obyectAccessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<ObyektSecondStepCustomer>> GetAll()
        {
            return new SuccessDataResult<List<ObyektSecondStepCustomer>>(obyectAccessCustomer.GetAll());
        }

        public IDataResult<ObyektSecondStepCustomer> GetById(int id)
        {
            return new SuccessDataResult<ObyektSecondStepCustomer>(obyectAccessCustomer.GetById(x => x.SecondStepCustomerId == id));
        }

        public IResult Update(ObyektSecondStepCustomer Model)
        {
            obyectAccessCustomer.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public IResult DeleteList(int ForeignId)
        {
            obyectAccessCustomer.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
