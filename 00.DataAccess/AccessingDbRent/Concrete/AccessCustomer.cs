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
    public class AccessCustomer : BaseRepository<SecondStepCustomer, MyDbContext>, IAccessCustomer
    {
        CustomerGeneric Customer;
        public AccessCustomer()
        {
            Customer = new CustomerGeneric();
        }
        public async void DeleteList(int foreignId)
        {
            await Customer.DeleteListGeneric<SecondStepCustomer>(foreignId, "SecondStepCustomerForeignId");
        }


        public async Task<List<string>> GetAll()
        {
          return  await Customer.GetAllCustomer<SecondStepCustomer>();
        }

        public async Task<List<SecondStepCustomer>> GetByIdList(int foreignId)
        {
           return await Customer.GetByIdListGeneric<SecondStepCustomer>(foreignId, "SecondStepCustomerForeignId");
        }
    }
}
