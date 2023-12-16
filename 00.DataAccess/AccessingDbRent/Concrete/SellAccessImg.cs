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
    public class SellAccessImg : BaseRepository<SellImg, MyDbContext>, ISellImg
    {
        public void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<SellImg> deleteList = context.Set<SellImg>().Where(x => x.ImgIdForeignId == foreignId).ToList();
                context.Set<SellImg>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }

        public List<SellImg> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<SellImg>().ToList();
            }
        }
    }
}

