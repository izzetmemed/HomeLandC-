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
    public class AccessCustomer : BaseRepository<SecondStepCustomer, MyDbContext>, IAccessCustomer
    {
        public void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<SecondStepCustomer> deleteList = context.Set<SecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToList();
                context.Set<SecondStepCustomer>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }


        public List<SecondStepCustomer> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<SecondStepCustomer>().ToList();
            }
        }
    }
}
