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
    public class ObyektAccess : BaseRepository<Obyekt, MyDbContext>, IObyekt
    {
        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Obyekts = await context.Set<Obyekt>().ToListAsync();
                var ObyektImg = await context.Set<ObyektImg>().ToListAsync();

                foreach (var sell in Obyekts)
                {
                    sell.ObyektImgs = ObyektImg.Where(img => img.ImgIdForeignId == sell.Id).ToList();
                }

                var allData = Obyekts;
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = item.Id,
                        Address = item.Address,
                        Room = item.Room,
                        Metro = item.Metro,
                        Price = item.Price,
                        Item = item.İtem,
                        Region = item.Region,
                        Area = item.Area,
                        Date = item.Date,
                        Document = item.Document,
                        SellorRent=item.SellOrRent,
                        Img = item.ObyektImgs.Select(x => x.ImgPath).ToList()
                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                data.Reverse();
                return data;
            }
        }
    }
}
