using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class MediaAccess : BaseRepository<Medium, MyDbContext>, IBaseRepository<Medium>
    {
        public async Task<List<Medium>> GetAllNormal(RentHome data)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Media = await context.Set<Medium>().ToListAsync();
                var suitableData = Media
                    .Where(x => x.Building == data.Building
                             && x.Metro == data.Metro
                             && x.Region == data.Region
                             && x.MinPrice < data.Price
                             && data.Price < x.MaxPrice)
                    .ToList();
                return suitableData;
            }
        }
    }
}
