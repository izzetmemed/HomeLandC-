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
    public class ObyectAccessCustomer : BaseRepository<ObyektSecondStepCustomer, MyDbContext>, IObyektCustomer
    {
        public void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<ObyektSecondStepCustomer> deleteList = context.Set<ObyektSecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToList();
                context.Set<ObyektSecondStepCustomer>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }

        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var customer = await context.Set<ObyektSecondStepCustomer>().ToListAsync();
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

        public async Task<List<ObyektSecondStepCustomer>> GetByIdList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var deleteGetById = await context.Set<ObyektSecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToListAsync();
                return deleteGetById;
            }
        }

    }
}
