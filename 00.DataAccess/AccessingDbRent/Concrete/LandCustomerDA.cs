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
    public class LandCustomerDA : BaseRepository<LandCustomer, MyDbContext>, ILandCustomer
    {

        CustomerGeneric Customer;
        public LandCustomerDA()
        {
            Customer = new CustomerGeneric();
        }
        public async void DeleteList(int foreignId)
        {
            await Customer.DeleteListGeneric<LandCustomer>(foreignId, "SecondStepCustomerForeignId");
        }

        public async Task<List<string>> GetAll()
        {
            return await Customer.GetAllCustomer<LandCustomer>();
        }

        public async Task<List<LandCustomer>> GetByIdList(int foreignId)
        {
            return await Customer.GetByIdListGeneric<LandCustomer>(foreignId, "SecondStepCustomerForeignId");
        }
    }
}
