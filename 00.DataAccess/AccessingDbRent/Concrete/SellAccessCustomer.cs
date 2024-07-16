using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete.Generic;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class SellAccessCustomer : BaseRepository<SellSecondStepCustomer, MyDbContext>, ISellCustomer
    {
        CustomerGeneric Customer;
        public SellAccessCustomer()
        {
            Customer = new CustomerGeneric();
        }
        public async void DeleteList(int foreignId)
        {
            await Customer.DeleteListGeneric<SellSecondStepCustomer>(foreignId, "SecondStepCustomerForeignId");
        }

        public async Task<List<string>> GetAll()
        {
            return await Customer.GetAllCustomer<SellSecondStepCustomer>();
        }

        public async Task<List<SellSecondStepCustomer>> GetByIdList(int foreignId)
        {
            return await Customer.GetByIdListGeneric<SellSecondStepCustomer>(foreignId, "SecondStepCustomerForeignId");
        }

     
    }
}
