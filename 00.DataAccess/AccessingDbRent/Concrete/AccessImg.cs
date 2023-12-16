using _00.DataAccess.AccessingDb.Abstract;
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
    public class AccessImg : BaseRepository<ImgName, MyDbContext>, IAccessImg
    {
        public void DeleteList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<ImgName> deleteList = context.Set<ImgName>().Where(x => x.ImgIdForeignId == foreignId).ToList();
                context.Set<ImgName>().RemoveRange(deleteList);
                context.SaveChanges();
            }
        }

        public List<ImgName> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.Set<ImgName>().ToList();
            }

        }
    }
}
