using DataAccess.AccessingDbRent.Abstract;
using DataAccess.Repository;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class SellAccessCustomer : BaseRepository<SellSecondStepCustomer, MyDbContext>, ISellCustomer
    {
        public void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<SellSecondStepCustomer> deleteList = context.Set<SellSecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToList();
                context.Set<SellSecondStepCustomer>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }

        public List<SellSecondStepCustomer> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<SellSecondStepCustomer>().ToList();
            }
        }
    }
}
