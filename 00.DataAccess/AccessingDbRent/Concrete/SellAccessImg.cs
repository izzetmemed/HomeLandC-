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

        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var allData = await context.Set<SellImg>().ToListAsync();
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

        public async Task<List<SellImg>> GetByIdList(int foreignId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var deleteGetById = await context.Set<SellImg>().Where(x => x.ImgIdForeignId == foreignId).ToListAsync();
                return deleteGetById;
            }
        }
    }
}

