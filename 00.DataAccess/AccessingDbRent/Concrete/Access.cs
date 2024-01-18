using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _00.DataAccess.AccessingDb.Abstract;
using Core;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;

namespace DataAccess.AccessingDb.Concrete
{
    public class Access : BaseRepository<RentHome, MyDbContext>, IAccess
    {
        public async Task<List<string>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var rentHomes = await context.Set<RentHome>().ToListAsync();
                var imgNames = await context.Set<ImgName>().ToListAsync();
                foreach (var rentHome in rentHomes)
                {
                    rentHome.ImgNames =  imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
                }

                var allData = rentHomes;
                List<string> data = new List<string>();

                foreach (var item in allData)
                {
                    var needData = new
                    {
                        Id = item.Id,
                        Address = item.Address,
                        Room = item.Room,
                        Metro=item.Metro,
                        Price=item.Price,
                        Item=item.İtem,
                        Region=item.Region,
                        Building=item.Building,
                        Area=item.Area,
                        Date=item.Date,
                        Img = item.ImgNames.Select(x => x.ImgPath).ToList()
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
                var rentHomes = await context.Set<RentHome>().ToListAsync();
                var imgNames = await context.Set<ImgName>().ToListAsync();
                var secondStepCustomers = await context.Set<SecondStepCustomer>().ToListAsync();



                foreach (var rentHome in rentHomes)
                {
                    rentHome.ImgNames = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
                    rentHome.SecondStepCustomers = secondStepCustomers
                        .Where(customer => customer.SecondStepCustomerForeignId == rentHome.Id)
                        .ToList();
                }

                var allData = rentHomes;
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
                        Bed = item.Bed,
                        Wardrobe = item.Wardrobe,
                        TableChair = item.TableChair,
                        CentralHeating = item.CentralHeating,
                        GasHeating = item.GasHeating,
                        Combi = item.Combi,
                        Tv = item.Tv,
                        WashingClothes = item.WashingClothes,
                        AirConditioning = item.AirConditioning,
                        Sofa = item.Sofa,
                        Wifi = item.Wifi,
                        Addition = item.Addition,
                        Boy = item.Boy,
                        Girl = item.Girl,
                        WorkingBoy = item.WorkingBoy,
                        Family = item.Family,

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
                        Img = item.ImgNames.Select(x => x.ImgPath).ToList(),
                        Customer = item.SecondStepCustomers.Select(x => SerializeSecondStepCustomer(x)).ToList()
                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                data.Reverse();
                return data;
            }
        }
        private string SerializeSecondStepCustomer(SecondStepCustomer customer)
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
