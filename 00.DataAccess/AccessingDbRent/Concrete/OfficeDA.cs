using _00.DataAccess.AccessingDb.Abstract;
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
    public class OfficeDA : BaseRepository<Office, MyDbContext>, IOffice
    {
        AccessGeneric accessGeneric { get; set; }
        public OfficeDA()
        {
            accessGeneric = new AccessGeneric();
        }
        public async Task<List<string>> GetAll(Expression<Func<Office, bool>>? predicate = null)
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<Office> rentHomes;

                if (predicate == null)
                {
                    rentHomes = await context.Set<Office>().ToListAsync();
                }
                else
                {
                    rentHomes = await context.Set<Office>().Where(predicate).ToListAsync();
                }
                await LoadImagesAsync(rentHomes, context);
                var data = TransformToJson(rentHomes);
                data.Reverse();
                return data;
            }
        }
        public async Task<List<string>> GetAllCoordinate()
        {
            return await accessGeneric.GetAllCoordinateGeneric<Office>();
        }
        public async Task<SearchDTO> GetAllNormal(int Page, Expression<Func<Office, bool>>? predicate = null)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Data = new List<Office>();
                var Pagination = 20;
                if (predicate == null)
                {
                    int pageSize = 20;
                    var rentHomes = await context.Set<Office>().OrderByDescending(r => r.Id).ToListAsync();
                    Data = rentHomes
                       .Skip((Page - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
                    Pagination = rentHomes.Count();
                }
                else
                {
                    Data = await context.Set<Office>().Where(predicate).ToListAsync();
                }
                var imgNames = await context.Set<OfficeImg>().ToListAsync();
                var secondStepCustomers = await context.Set<OfficeCustomer>().ToListAsync();
                foreach (var rentHome in Data)
                {
                    rentHome.OfficeImgs = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
                    rentHome.OfficeCustomers = secondStepCustomers
                        .Where(customer => customer.SecondStepCustomerForeignId == rentHome.Id)
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
                        CoordinateX = item.CoordinateX,
                        CoordinateY = item.CoordinateY,
                        Repair = item.Repair,
                        Addition = item.Addition,
                        Document=item.Document,
                        SellOrRent=item.SellOrRent,
                        Address = item.Address,
                        Looking = item.Looking,
                        Email = item.Email,
                        Room = item.Room,
                        Recommend=item.Recommend,
                        Metro = item.Metro,
                        Price = item.Price,
                        Item = item.Item,
                        Region = item.Region,
                        Area = item.Area,
                        Date = item.Date,
                        IsCalledWithHomeOwnFirstStep = item.IsCalledWithHomeOwnFirstStep,
                        IsCalledWithCustomerFirstStep = item.IsCalledWithCustomerFirstStep,
                        IsPaidHomeOwnFirstStep = item.IsPaidHomeOwnFirstStep,
                        IsPaidCustomerFirstStep = item.IsPaidCustomerFirstStep,
                        IsCalledWithHomeOwnThirdStep = item.IsCalledWithHomeOwnThirdStep,
                        Img = item.OfficeImgs.Select(x => x.ImgPath).ToList(),
                        Customer = item.OfficeCustomers.Select(x => SerializeSecondStepCustomer(x)).ToList()
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
        private string SerializeSecondStepCustomer(OfficeCustomer customer)
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
                var rentHomes = await context.Set<Office>().ToListAsync();
                var imgNames = await context.Set<OfficeImg>().ToListAsync();
                foreach (var rentHome in rentHomes)
                {
                    rentHome.OfficeImgs = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
                }
                var allData = rentHomes.Where(x => x.Recommend == true && x.IsPaidHomeOwnFirstStep == false);
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
                        Item = item.Item,
                        Region = item.Region,
                        Repair = item.Repair,
                        Area = item.Area,
                        Date = item.Date,
                        SellOrRent = item.SellOrRent,
                        Img = item.OfficeImgs.Select(x => x.ImgPath).ToList()
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
                var rentHomes = await context.Set<Office>().OrderByDescending(r => r.Id).ToListAsync();
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
        private IQueryable<Office> BuildSuitableDataQuery(SearchModel searchModel, MyDbContext _context)
        {
            return _context.Set<Office>()
                .Where(x =>
                    (searchModel.Building.Length == 0 || searchModel.Building.Contains(x.SellOrRent)) &&
                    (searchModel.Metro.Length == 0 || searchModel.Metro.Contains(x.Metro)) &&
                    (searchModel.Room.Length == 0 || searchModel.Room.Contains(x.Room.ToString())) &&
                    (searchModel.Region.Length == 0 || searchModel.Region.Contains(x.Region)) &&
                    searchModel.MinPrice <= x.Price &&
                    searchModel.MaxPrice >= x.Price);
        }
        private async Task LoadImagesAsync(IEnumerable<Office> rentHomesSearch, MyDbContext _context)
        {
            var imgNames = await _context.Set<OfficeImg>().ToListAsync();
            foreach (var rentHome in rentHomesSearch)
            {
                rentHome.OfficeImgs = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
            }
        }
        private List<string> TransformToJson(IEnumerable<Office> rentHomesSearch)
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
                    Item = item.Item,
                    Region = item.Region,
                    Repair = item.Repair,
                    Area = item.Area,
                    Date = item.Date,
                    SellOrRent = item.SellOrRent,
                    Img = item.OfficeImgs.Select(x => x.ImgPath).ToList()
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
