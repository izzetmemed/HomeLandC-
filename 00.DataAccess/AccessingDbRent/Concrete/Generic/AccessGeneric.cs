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

namespace DataAccess.AccessingDbRent.Concrete.Generic
{

    internal class AccessGeneric
    {
        GetValuGeneric valuGeneric;
        public AccessGeneric()
        {
            valuGeneric = new GetValuGeneric();
        }
        public string SerializeSecondStepCustomer<T>(T obj)
        {
            var serializedObject = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                serializedObject[property.Name] = JsonSerializer.Serialize(property.GetValue(obj));

            }

            return JsonSerializer.Serialize(serializedObject);
        }
        public async Task<List<string>> GetAllCoordinateGeneric<T>() where T : class 
        {
            using (MyDbContext context = new MyDbContext())
            {
                var rentHomes = await context.Set<T>().ToListAsync();

                List<string> data = new List<string>();

                foreach (var item in rentHomes)
                {
                    var needData = new
                    {
                        Id = valuGeneric.GetPropertyValue(item, "Id"),
                        CoordinateX =valuGeneric.GetPropertyValue(item, "CoordinateX"),
                        CoordinateY = valuGeneric.GetPropertyValue(item, "CoordinateY"),

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
