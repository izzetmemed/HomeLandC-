using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using _00.DataAccess.AccessingDb.Abstract;
using _00.DataAccess.AccessingDbRent.Abstract;
using CloudinaryDotNet.Actions;
using Core;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete.Generic;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DTOmodels;
using Model.Models;

namespace DataAccess.AccessingDb.Concrete
{
    public class Access : BaseRepository<RentHome, MyDbContext>, IAccess
    {
        AccessGeneric accessGeneric { get; set; }
        public Access()
        {
            accessGeneric= new AccessGeneric();
        }
        public async Task<List<string>> GetAll(Expression<Func<RentHome, bool>>? predicate = null  )
        {
            using (MyDbContext context = new MyDbContext())
            {
                List<RentHome> rentHomes;

                if (predicate == null)
                {
                    rentHomes = await context.Set<RentHome>().ToListAsync();
                }
                else
                {
                    rentHomes = await context.Set<RentHome>().Where(predicate).ToListAsync();
                }

                await LoadImagesAsync(rentHomes, context);
                var data = TransformToJson(rentHomes);
                data.Reverse();
                return data;
            }
        }

        public async Task<List<string>> GetAllCoordinate()
        {
            return await accessGeneric.GetAllCoordinateGeneric<RentHome>();
        }
        public async Task<SearchDTO> GetAllNormal(int Page, Expression<Func<RentHome, bool>>? predicate = null)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Data = new List<RentHome>();
                var Pagination = 20;
                if (predicate == null)
                {
                    int pageSize = 20;
                    var rentHomes = await context.Set<RentHome>().OrderByDescending(r => r.Id).ToListAsync();
                    Data = rentHomes
                       .Skip((Page - 1) * pageSize)
                       .Take(pageSize)
                       .ToList();
                    Pagination = rentHomes.Count();
                }
                else
                {
                    Data = await context.Set<RentHome>().Where(predicate).ToListAsync();
                }
                var imgNames = await context.Set<ImgName>().ToListAsync();
                var secondStepCustomers = await context.Set<SecondStepCustomer>().ToListAsync();
                foreach (var rentHome in Data)
                {
                    rentHome.ImgNames = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
                    rentHome.SecondStepCustomers = secondStepCustomers
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
                        Recommend=item.Recommend,
                        Wifi = item.Wifi,
                        Addition = item.Addition,
                        Boy = item.Boy,
                        Girl = item.Girl,
                        Looking=item.Looking,
                        Email=item.Email,
                        WorkingBoy = item.WorkingBoy,
                        Family = item.Family,
                        Address = item.Address,
                        Room = item.Room,
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
                        Img = item.ImgNames.Select(x => x.ImgPath).ToList(),
                        Customer = item.SecondStepCustomers.Select(x => SerializeSecondStepCustomer(x)).ToList()
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
        public async Task<List<string>> GetAllRecommend()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var rentHomes = await context.Set<RentHome>().ToListAsync();
                var imgNames = await context.Set<ImgName>().ToListAsync();
                await LoadImagesAsync(rentHomes, context);
               
                var allData = rentHomes.Where(x => x.Recommend == true && x.IsPaidHomeOwnFirstStep == false);
                var data = TransformToJson(allData);
                data.Reverse();
                return data;
            }
        }
        public async Task<SearchDTO> GetAllPage(int Page)
        {
            using (MyDbContext context = new MyDbContext())
            {
                int pageSize = 20;
                var rentHomes =await context.Set<RentHome>().OrderByDescending(r => r.Id).ToListAsync();
                var Data = rentHomes
                    .Skip((Page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                var Pagination =rentHomes.Count();
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
        public async Task<SearchDTO> GetAllSearch(SearchModel searchModel, int Page)
        {
            using (MyDbContext context = new MyDbContext())
            {
                int pageSize = 20;
                var suitableDataQuery = BuildSuitableDataQuery(searchModel,context);
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
        private IQueryable<RentHome> BuildSuitableDataQuery(SearchModel searchModel, MyDbContext _context)
        {
            return _context.Set<RentHome>()
                .Where(x =>
                    (searchModel.Building.Length == 0 || searchModel.Building.Contains(x.Building)) &&
                    (searchModel.Metro.Length == 0 || searchModel.Metro.Contains(x.Metro)) &&
                    (searchModel.Room.Length == 0 || searchModel.Room.Contains(x.Room.ToString())) &&
                    (searchModel.Region.Length == 0 || searchModel.Region.Contains(x.Region)) &&
                    searchModel.MinPrice <= x.Price &&
                    searchModel.MaxPrice >= x.Price);
        }
        private async Task LoadImagesAsync(IEnumerable<RentHome> rentHomesSearch, MyDbContext _context)
        {
            var imgNames = await _context.Set<ImgName>().ToListAsync();
            foreach (var rentHome in rentHomesSearch)
            {
                rentHome.ImgNames = imgNames.Where(img => img.ImgIdForeignId == rentHome.Id).ToList();
            }
        }
        private List<string> TransformToJson(IEnumerable<RentHome> rentHomesSearch)
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
                    Building = item.Building,
                    Area = item.Area,
                    Date = item.Date,
                    Img = item.ImgNames.Select(x => x.ImgPath).ToList()
                };
                return JsonSerializer.Serialize(needData);
            }).ToList();
        }
    }
}
