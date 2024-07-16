using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OfficeCustomerOperation : IOfficeCustomerService
    {
        OfficeCustomerDA AccessCustomer ;
        OfficeOperation OfficeOperation;
        SendMail SendMail;
        CustomerEmailDA customerEmailDA;
        public OfficeCustomerOperation()
        {
            AccessCustomer = new OfficeCustomerDA();
            SendMail = new SendMail();
            OfficeOperation = new OfficeOperation();
            customerEmailDA = new CustomerEmailDA();

        }
        public async Task<IResult> Add(OfficeCustomer Model)
        {
            if (string.IsNullOrWhiteSpace(Model.Number) && string.IsNullOrWhiteSpace(Model.FullName))
            {
                return new ErrorResult(MyMessage.Error);
            }
            AccessCustomer.Add(Model);
            int ModelId = Model.SecondStepCustomerForeignId ?? 0;
            if (ModelId != 0)
            {
                var data = await OfficeOperation.GetById(ModelId);
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

        public async Task<IResult> Delete(OfficeCustomer Model)
        {
            AccessCustomer.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> DeleteList(int ForeignId)
        {
            AccessCustomer.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
           
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await AccessCustomer.GetAll());
        }

        public async Task<IDataResult<OfficeCustomer>> GetById(int id)
        {
            return new SuccessDataResult<OfficeCustomer>(AccessCustomer.GetById(x => x.SecondStepCustomerId == id));
        }

        public async Task<IDataResult<List<OfficeCustomer>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<OfficeCustomer>>(await AccessCustomer.GetByIdList(id));
        }

        public async Task<IResult> Update(OfficeCustomer Model)
        {
            AccessCustomer.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
