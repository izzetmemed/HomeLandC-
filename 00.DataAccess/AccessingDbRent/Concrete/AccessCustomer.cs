using DataAccess.AccessingDbRent.Abstract;
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
        public async void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<SecondStepCustomer> deleteList =await context.Set<SecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToListAsync();
                context.Set<SecondStepCustomer>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }


        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var customer = await context.Set<SecondStepCustomer>().ToListAsync();
                var allData = customer;
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = item.SecondStepCustomerId,
                        Fullname = item.FullName,
                        Number = item.Number,
                        Date = item.DirectCustomerDate,
                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                return data;
            }
        }

        public async Task<List<SecondStepCustomer>> GetByIdList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var deleteGetById = await context.Set<SecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToListAsync();
                return deleteGetById;
            }
        }
    }
}
