//using DataAccess.AccessingDbRent.Abstract;
//using DataAccess.Repository;
//using Model.Contexts;
//using Model.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess.AccessingDbRent.Concrete
//{
//    public class ObyectAccessCustomer : BaseRepository<ObyektSecondStepCustomer, MyDbContext>, IObyektCustomer
//    {
//        public void DeleteList(int foreignId)
//        {
//            using (MyDbContext context = new MyDbContext())
//            {
//                List<ObyektSecondStepCustomer> deleteList = context.Set<ObyektSecondStepCustomer>().Where(x => x.SecondStepCustomerForeignId == foreignId).ToList();
//                context.Set<ObyektSecondStepCustomer>().RemoveRange(deleteList);
//                context.SaveChanges();
//            }
//        }

//        public List<ObyektSecondStepCustomer> GetAll()
//        {
//            using (MyDbContext context = new MyDbContext())
//            {
//                return context.Set<ObyektSecondStepCustomer>().ToList();
//            }
//        }
//    }
//}
