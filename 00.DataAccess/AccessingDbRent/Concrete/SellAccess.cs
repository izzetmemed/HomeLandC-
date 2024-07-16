 using _00.DataAccess.AccessingDbRent.Abstract;
using CloudinaryDotNet.Actions;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete.Generic;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DTOmodels;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class SellAccess : BaseRepository<Sell, MyDbContext>, ISell
    {
        AccessGeneric accessGeneric { get; set; }
        public SellAccess()
        {
            accessGeneric = new AccessGeneric();
        }
        public async Task<List<string>> GetAll(Expression<Func<Sell, bool>>? predicate = null)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<Sell> rentHomes;

                if (predicate == null)
                {
                    rentHomes = await context.Set<Sell>().ToListAsync();
                }
                else
                {
                    rentHomes = await context.Set<Sell>().Where(predicate).ToListAsync();
                }
                await LoadImagesAsync(rentHomes, context);
                var data = TransformToJson(rentHomes);
                data.Reverse();
                return data;
            }
        }
        public async Task<List<string>> GetAllCoordinate()
        {
            return await accessGeneric.GetAllCoordinateGeneric<Sell>();
        }
        public async Task<SearchDTO> GetAllNormal(int Page, Expression<Func<Sell, bool>>? predicate = null)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Data = new List<Sell>();
                var Pagination = 20;
                if (predicate == null)
                {
                    int pageSize = 20;
                    var rentHomes = await context.Set<Sell>().OrderByDescending(r => r.Id).ToListAsync();
                    Data = rentHomes
                       .Skip((Page - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
                    Pagination = rentHomes.Count();
                }
                else
                {
                    Data = await context.Set<Sell>().Where(predicate).ToListAsync();
                }
                var img = await context.Set<SellImg>().ToListAsync();
                var customer = await context.Set<SellSecondStepCustomer>().ToListAsync();
                foreach (var sell in Data)
                {
                    sell.SellImgs = img.Where(img => img.ImgIdForeignId == sell.Id).ToList();
                    sell.SellSecondStepCustomers = customer
                    .Where(customer => customer.SecondStepCustomerForeignId == sell.Id)
                    .ToList();
                }
                List<string> data = new List<string>();
                foreach (var item in Data)
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
                        Recommend=item.Recommend,
                        Room = item.Room,
                        Metro = item.Metro,
                        Price = item.Price,
                        Item = item.Item,
                        Document=item.Document,
                        Looking = item.Looking,
                        Email = item.Email,
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
                var info = new SearchDTO()
                {
                    PaginationCount = Pagination,
                    Data = data
                };
                return info;
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
        public async Task<List<string>> GetAllRecommend()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Sells = await context.Set<Sell>().ToListAsync();
                var SellImg = await context.Set<SellImg>().ToListAsync();

                foreach (var sell in Sells)
                {
                    sell.SellImgs = SellImg.Where(img => img.ImgIdForeignId == sell.Id).ToList();

                }
                var allData = Sells.Where(x=>x.Recommend==true && x.IsPaidHomeOwnFirstStep==false);
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
                        Region = item.Region,
                        VideoPath = item.VideoPath,
                        Area = item.Area,
                        Building = item.Building,
                        Date = item.Date,
                        Document = item.Document,
                        Img = item.SellImgs.Select(x => x.ImgPath).ToList()
                    };
                    string jsonData = JsonSerializer.Serialize(needData);
                    data.Add(jsonData);
                }
                data.Reverse();
                return data;
            }
        }
        public async Task<SearchDTO> GetAllPage(int Page)
        {
            using (MyDbContext context = new MyDbContext())
            {
                int pageSize = 20;
                var rentHomes = await context.Set<Sell>().OrderByDescending(r => r.Id).ToListAsync();
                var Data = rentHomes
                    .Skip((Page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                var Pagination = rentHomes.Count();
                await LoadImagesAsync(Data, context);
                var data = TransformToJson(Data);
                var info = new SearchDTO()
                {
                    PaginationCount = Pagination,
                    Data = data
                };
                return info;
            }
        }

        private IQueryable<Sell> BuildSuitableDataQuery(SearchModel searchModel, MyDbContext _context)
        {
            return _context.Set<Sell>()
                .Where(x =>
                    (searchModel.Building.Length == 0 || searchModel.Building.Contains(x.Building)) &&
                    (searchModel.Metro.Length == 0 || searchModel.Metro.Contains(x.Metro)) &&
                    (searchModel.Room.Length == 0 || searchModel.Room.Contains(x.Room.ToString())) &&
                    (searchModel.Region.Length == 0 || searchModel.Region.Contains(x.Region)) &&
                    searchModel.MinPrice <= x.Price &&
                    searchModel.MaxPrice >= x.Price);
        }
        private async Task LoadImagesAsync(IEnumerable<Sell> rentHomesSearch, MyDbContext _context)
        {
            var imgNames = await _context.Set<SellImg>().ToListAsync();
            foreach (var rentHome in rentHomesSearch)
            {
                rentHome.SellImgs = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
            }
        }
        private List<string> TransformToJson(IEnumerable<Sell> rentHomesSearch)
        {
            return rentHomesSearch.Select(item =>
            {
                var needData = new
                {
                    Id = item.Id,
                    Address = item.Address,
                    Room = item.Room,
                    Metro = item.Metro,
                    Price = item.Price,
                    Region = item.Region,
                    VideoPath = item.VideoPath,
                    Area = item.Area,
                    Building = item.Building,
                    Date = item.Date,
                    Document = item.Document,
                    Img = item.SellImgs.Select(x => x.ImgPath).ToList()
                };
                return JsonSerializer.Serialize(needData);
            }).ToList();
        }

        public async Task<SearchDTO> GetAllSearch(SearchModel searchModel, int Page)
        {
            using (MyDbContext context = new MyDbContext())
            {
                int pageSize = 20;
                var suitableDataQuery = BuildSuitableDataQuery(searchModel, context);
                int Pagination = await suitableDataQuery.CountAsync();
                var rentHomesSearch = await suitableDataQuery
                .OrderByDescending(r => r.Id)
                .Skip((Page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                await LoadImagesAsync(rentHomesSearch, context);
                var data = TransformToJson(rentHomesSearch);
                var info = new SearchDTO()
                {
                    PaginationCount = Pagination,
                    Data = data
                };
                return info;
            }
        }
    }
}
