 using _00.DataAccess.AccessingDbRent.Abstract;
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
    public class SellAccess : BaseRepository<Sell, MyDbContext>, ISell
    {

        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Sells = await context.Set<Sell>().ToListAsync();
                var SellImg = await context.Set<SellImg>().ToListAsync();




                foreach (var sell in Sells)
                {
                    sell.SellImgs = SellImg.Where(img => img.ImgIdForeignId == sell.Id).ToList();
                 
                }

                var allData = Sells;
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
                        Document=item.Document,
                        Img = item.SellImgs.Select(x => x.ImgPath).ToList()
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
                var Sells = await context.Set<Sell>().ToListAsync();
                var img = await context.Set<SellImg>().ToListAsync();
                var customer = await context.Set<SellSecondStepCustomer>().ToListAsync();



                foreach (var sell in Sells)
                {
                    sell.SellImgs = img.Where(img => img.ImgIdForeignId == sell.Id).ToList();
                    sell.SellSecondStepCustomers = customer
                        .Where(customer => customer.SecondStepCustomerForeignId == sell.Id)
                        .ToList();
                }

                var allData = Sells;
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = item.Id,
                        FullName = item.Fullname,
                        Number = item.Number,
                        Floor = item.Floor,
                        CoordinateX = item.CoordinateX,
                        CoordinateY = item.CoordinateY,
                        Repair = item.Repair,
                        Building = item.Building,
                        Addition = item.Addition,
                        Address = item.Address,
                        Room = item.Room,
                        Metro = item.Metro,
                        Price = item.Price,
                        Item = item.İtem,
                        Region = item.Region,
                        Area = item.Area,
                        Date = item.Date,
                        IsCalledWithHomeOwnFirstStep = item.IsCalledWithHomeOwnFirstStep,
                        IsCalledWithCustomerFirstStep = item.IsCalledWithCustomerFirstStep,
                        IsPaidHomeOwnFirstStep = item.IsPaidHomeOwnFirstStep,
                        IsPaidCustomerFirstStep = item.IsPaidCustomerFirstStep,
                        IsCalledWithHomeOwnThirdStep = item.IsCalledWithHomeOwnThirdStep,
                        Img = item.SellImgs.Select(x => x.ImgPath).ToList(),
                        Customer = item.SellSecondStepCustomers.Select(x => SerializeSecondStepCustomer(x)).ToList()
                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                data.Reverse();
                return data;
            }
        }
        private string SerializeSecondStepCustomer(SellSecondStepCustomer customer)
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
