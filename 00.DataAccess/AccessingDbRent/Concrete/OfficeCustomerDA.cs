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
    public class OfficeCustomerDA : BaseRepository<OfficeCustomer, MyDbContext>, IOfficeCustomer
    {

        CustomerGeneric Customer;
        public OfficeCustomerDA()
        {
            Customer = new CustomerGeneric();
        }
        public async void DeleteList(int foreignId)
        {
            await Customer.DeleteListGeneric<OfficeCustomer>(foreignId, "SecondStepCustomerForeignId");
        }

        public async Task<List<string>> GetAll()
        {
            return await Customer.GetAllCustomer<OfficeCustomer>();
        }

        public async Task<List<OfficeCustomer>> GetByIdList(int foreignId)
        {
            return await Customer.GetByIdListGeneric<OfficeCustomer>(foreignId, "SecondStepCustomerForeignId");
        }
    }
}
