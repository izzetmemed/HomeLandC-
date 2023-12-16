using _00.DataAccess.AccessingDb.Abstract;
using CloudinaryDotNet.Actions;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.AccessingDb.Concrete
{
    public class Access : BaseRepository<RentHome, MyDbContext>, IAccess
    {
        public List<RentHome> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
               var result =  from rentHome in context.Set<RentHome>()
                             join imgNames in context.Set<ImgName>() on rentHome.Id equals imgNames.ImgIdForeignId into imgNamesGroup
                             from imgName in imgNamesGroup.DefaultIfEmpty()
                             join customer in context.Set<SecondStepCustomer>() on rentHome.Id equals customer.SecondStepCustomerForeignId into customersGroup
                             from customer in customersGroup.DefaultIfEmpty()
                             group rentHome by rentHome.Id into groupedRentHomes
                             orderby groupedRentHomes.Key descending
                             select new RentHome
                             {
                                 Id = groupedRentHomes.Key,
                                 Address = groupedRentHomes.FirstOrDefault().Address,
                                 Price=groupedRentHomes.FirstOrDefault().Price,
                                 Metro=groupedRentHomes.FirstOrDefault().Metro,
                                 İtem=groupedRentHomes.FirstOrDefault().İtem,
                                 Room=groupedRentHomes.FirstOrDefault().Room,
                                 Region=groupedRentHomes.FirstOrDefault().Region,
                                 Area=groupedRentHomes.FirstOrDefault().Area,
                                 Date=groupedRentHomes.FirstOrDefault().Date,
                                 ImgNames =groupedRentHomes.FirstOrDefault().ImgNames,
                                 SecondStepCustomers=groupedRentHomes.FirstOrDefault().SecondStepCustomers
               
                             };

                return result.ToList();
            }
        }
    }
}
