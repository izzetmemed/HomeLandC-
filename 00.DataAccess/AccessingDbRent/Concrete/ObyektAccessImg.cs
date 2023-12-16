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
    public class ObyektAccessImg : BaseRepository<ObyektImg, MyDbContext>, IObyektImg
    {
        public void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<ObyektImg> deleteList = context.Set<ObyektImg>().Where(x => x.ImgIdForeignId == foreignId).ToList();
                context.Set<ObyektImg>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }

        public List<ObyektImg> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<ObyektImg>().ToList();
            }
        }
    }
}
