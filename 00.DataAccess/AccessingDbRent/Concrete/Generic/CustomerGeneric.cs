using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.AccessingDbRent.Concrete.Generic
{
    internal class CustomerGeneric
    {
        GetValuGeneric valuGeneric;
        public CustomerGeneric()
        {
            valuGeneric = new GetValuGeneric();
        }
        public async Task DeleteListGeneric<T>(int foreignId,string name) where T : class
        {
            using (MyDbContext context = new MyDbContext())
            {
                var deleteList = await context.Set<T>().ToListAsync();
                var foreignIdPropertyName = name; 

                var foreignIdProperty = typeof(T).GetProperty(foreignIdPropertyName);
                if (foreignIdProperty == null)
                {
                    throw new InvalidOperationException($"Entity type {typeof(T).Name} does not have a property named {foreignIdPropertyName}.");
                }
                deleteList = deleteList
                .Where(x => {
                    var foreignIdPropertyValue = foreignIdProperty.GetValue(x);
                    if (foreignIdPropertyValue != null && int.TryParse(foreignIdPropertyValue.ToString(), out int parsedValue))
                    {
                        return parsedValue == foreignId;
                    }
                    return false;
                })
                .ToList();

                context.Set<T>().RemoveRange(deleteList);
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<T>> GetByIdListGeneric<T>(int foreignId,string name) where T:class
        {
            using (MyDbContext context = new MyDbContext())
            {
                var deleteList = await context.Set<T>().ToListAsync();
                var foreignIdPropertyName = name;
                var foreignIdProperty = typeof(T).GetProperty(foreignIdPropertyName);
                if (foreignIdProperty == null)
                {
                    throw new InvalidOperationException($"Entity type {typeof(T).Name} does not have a property named {foreignIdPropertyName}.");
                }

                deleteList = deleteList
                .Where(x => {
                var foreignIdPropertyValue = foreignIdProperty.GetValue(x);
                if (foreignIdPropertyValue != null && int.TryParse(foreignIdPropertyValue.ToString(), out int parsedValue))
                {
                return parsedValue == foreignId;
                    }
                return false;
                })
                .ToList();
                return deleteList;
            }
        }
        public async Task<List<string>> GetAllCustomer<T>()  where T : class
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
                        Fullname = valuGeneric.GetPropertyValue(item, "FullName"),
                        Number = valuGeneric.GetPropertyValue(item, "Number"),
                        Date = valuGeneric.GetPropertyValue(item, "DirectCustomerDate"),
                    };

                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                return data;
            }

        }
    }
}
