using _00.DataAccess.AccessingDb.Abstract;
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

        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var allData = await context.Set<ImgName>().ToListAsync();
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = item.ImgId,
                        ForeignID = item.ImgIdForeignId,
                        Path = item.ImgPath,

                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                return data;
            }

        }

        public async Task<List<ImgName>> GetByIdList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var deleteGetById = await context.Set<ImgName>().Where(x => x.ImgIdForeignId == foreignId).ToListAsync();
                return deleteGetById;
            }
        }

    }
}