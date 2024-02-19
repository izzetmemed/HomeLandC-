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

        public async Task<List<string>> GetAllCoordinate()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var rentHomes = await context.Set<Obyekt>().ToListAsync();


                var allData = rentHomes;
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = item.Id,
                        CoordinateX = item.CoordinateX,
                        CoordinateY = item.CoordinateY,

                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                data.Reverse();
                return data;
            }
        }

        public async Task<List<string>> GetAllNormal()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Obyekts = await context.Set<Obyekt>().ToListAsync();
                var ObyektImg = await context.Set<ObyektImg>().ToListAsync();
                var secondStepCustomers = await context.Set<ObyektSecondStepCustomer>().ToListAsync();

                foreach (var sell in Obyekts)
                {
                    sell.ObyektImgs = ObyektImg.Where(img => img.ImgIdForeignId == sell.Id).ToList();
                    sell.ObyektSecondStepCustomers = secondStepCustomers
                        .Where(customer => customer.SecondStepCustomerForeignId == sell.Id)
                        .ToList();
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
                        İtem = item.İtem,
                        Region = item.Region,
                        Area = item.Area,
                        Number = item.Number,
                        Date = item.Date,
                        Fullname = item.Fullname,
                        CoordinateX = item.CoordinateX,
                        CoordinateY = item.CoordinateY,
                        Repair = item.Repair,
                        Addition = item.Addition,
                        Document = item.Document,
                        SellorRent = item.SellOrRent,
                        IsCalledWithHomeOwnFirstStep = item.IsCalledWithHomeOwnFirstStep,
                        IsCalledWithCustomerFirstStep = item.IsCalledWithCustomerFirstStep,
                        IsPaidHomeOwnFirstStep = item.IsPaidHomeOwnFirstStep,
                        IsPaidCustomerFirstStep = item.IsPaidCustomerFirstStep,
                        IsCalledWithHomeOwnThirdStep = item.IsCalledWithHomeOwnThirdStep,
                        Img = item.ObyektImgs.Select(x => x.ImgPath).ToList(),
                         Customer = item.ObyektSecondStepCustomers.Select(x => SerializeSecondStepCustomer(x)).ToList()
                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                data.Reverse();
                return data;
            }
        }

        private string SerializeSecondStepCustomer(ObyektSecondStepCustomer customer)
        {
            var serializedCustomer = new
            {
                FullName = JsonSerializer.Serialize(customer.FullName),
                Number = JsonSerializer.Serialize(customer.Number),
                DirectCustomerDate = JsonSerializer.Serialize(customer.DirectCustomerDate),
                SecondStepCustomerId = JsonSerializer.Serialize(customer.SecondStepCustomerId)
            };

            return JsonSerializer.Serialize(serializedCustomer);
        }
    }
}
