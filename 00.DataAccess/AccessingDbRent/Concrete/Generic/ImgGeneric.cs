using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete.Generic
{
    internal class ImgGeneric
    {
        GetValuGeneric valuGeneric;
        public ImgGeneric()
        {
            valuGeneric = new GetValuGeneric();
        }
        
        public async Task<List<string>> GetAllImgGeneric<T>() where T : class
        {
            using (MyDbContext context = new MyDbContext())
            {
                var allData = await context.Set<T>().ToListAsync();
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = valuGeneric.GetPropertyValue(item, "SecondStepCustomerId"),
                        ForeignID = valuGeneric.GetPropertyValue(item, "ImgIdForeignId"),
                        Path = valuGeneric.GetPropertyValue(item, "Path"),

                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                return data;
            }
        }
    }
}
