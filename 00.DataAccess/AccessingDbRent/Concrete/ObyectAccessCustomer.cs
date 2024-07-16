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
    public class ObyectAccessCustomer : BaseRepository<ObyektSecondStepCustomer, MyDbContext>, IObyektCustomer
    {
        CustomerGeneric Customer;
        public ObyectAccessCustomer()
        {
            Customer = new CustomerGeneric();
        }
        public async void DeleteList(int foreignId)
        {
            await Customer.DeleteListGeneric<ObyektSecondStepCustomer>(foreignId, "SecondStepCustomerForeignId");
        }

        public async Task<List<string>> GetAll()
        {
            return await Customer.GetAllCustomer<ObyektSecondStepCustomer>();
        }

        public async Task<List<ObyektSecondStepCustomer>> GetByIdList(int foreignId)
        {
            return await Customer.GetByIdListGeneric<ObyektSecondStepCustomer>(foreignId, "SecondStepCustomerForeignId");
        }

    }
}
