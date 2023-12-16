using _00.DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.Repository;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class SellAccess : BaseRepository<Sell, MyDbContext>, ISell
    {
        public List<Sell> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var result = from rentHome in context.Set<Sell>()
                             join imgName in context.Set<SellImg>() on rentHome.Id equals imgName.ImgIdForeignId into imgNamesGroup
                             from imgName in imgNamesGroup.DefaultIfEmpty()
                             join customer in context.Set<SellSecondStepCustomer>() on rentHome.Id equals customer.SecondStepCustomerForeignId into customersGroup
                             from customer in customersGroup.DefaultIfEmpty()
                             select new Sell
                             {
                                 Id = rentHome.Id,
                                 Fullname = rentHome.Fullname,
                                 Number = rentHome.Number,
                                 Region = rentHome.Region,
                                 Address = rentHome.Address,
                                 Floor = rentHome.Floor,
                                 Metro = rentHome.Metro,
                                 CoordinateX = rentHome.CoordinateX,
                                 CoordinateY = rentHome.CoordinateY,
                                 Room = rentHome.Room,
                                 Repair = rentHome.Repair,
                                 Building = rentHome.Building,
                                 İtem = rentHome.İtem,
                                 Area = rentHome.Area,
                                 Date = rentHome.Date,
                                 Price = rentHome.Price,
                                 Addition = rentHome.Addition,
                                 IsCalledWithHomeOwnFirstStep = rentHome.IsCalledWithHomeOwnFirstStep,
                                 IsCalledWithCustomerFirstStep = rentHome.IsCalledWithCustomerFirstStep,
                                 IsPaidHomeOwnFirstStep = rentHome.IsPaidHomeOwnFirstStep,
                                 IsPaidCustomerFirstStep = rentHome.IsPaidCustomerFirstStep,
                                 IsCalledWithHomeOwnThirdStep = rentHome.IsCalledWithHomeOwnThirdStep,
                                 SellImgs = imgName == null ? null : new List<SellImg>
                                 {
                                     new SellImg
                                     {
                                         ImgId = imgName.ImgId,
                                         ImgIdForeignId = imgName.ImgIdForeignId,
                                         ImgPath = imgName.ImgPath,
                                         ImgIdForeign = imgName.ImgIdForeign
                                     }
                                 }.ToList(),
                                 SellSecondStepCustomers = customer == null ? null : new List<SellSecondStepCustomer>
                                 {
                                     new SellSecondStepCustomer
                                     {
                                         SecondStepCustomerId = customer.SecondStepCustomerId,
                                         SecondStepCustomerForeignId = customer.SecondStepCustomerForeignId,
                                         FullName = customer.FullName,
                                         Number = customer.Number,
                                         DirectCustomerDate = customer.DirectCustomerDate,
                                         SecondStepCustomerForeign = customer.SecondStepCustomerForeign
                                     }
                                 }.ToList()
                             };

                return result.ToList();
            }
        }
    }
}
