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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ObyektOperationCustomer : IObyektServiceCustomer
    {
        ObyectAccessCustomer obyectAccessCustomer;
        SendMail SendMail;
        ObyektOperation ObyektOperation;
        CustomerEmailDA customerEmailDA;
        public ObyektOperationCustomer()
        {
            obyectAccessCustomer = new ObyectAccessCustomer();
            SendMail = new SendMail();
            ObyektOperation = new ObyektOperation();
            customerEmailDA = new CustomerEmailDA();
        }
        public async Task<IResult> Add(ObyektSecondStepCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
            obyectAccessCustomer.Add(Model);
            int ModelId = Model.SecondStepCustomerForeignId ?? 0;
            if (ModelId != 0)
            {
                var data = await ObyektOperation.GetById(ModelId);
                if (!Regex.IsMatch(Model.Email, @"^\S+@\S+\.\S+$"))
                {
                    SendMail.SendEmail(data.Data.Email, data.Data.Fullname, Model.FullName);
                }
                else
                {
                    SendMail.SendEmail(data.Data.Email, data.Data.Fullname, Model.FullName);
                    SendMail.SendEmail(Model.Email, data.Data.Fullname, Model.FullName, data.Data.Number);
                    var customeremail = new CustomerEmail()
                    {
                        Email = Model.Email,
                        Fullname = Model.FullName,
                        Number = Model.Number
                    };
                    customerEmailDA.Add(customeremail);
                };
            }
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
