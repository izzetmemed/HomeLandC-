using _00.DataAccess.AccessingDb.Abstract;
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
    public class ObyektAccess : BaseRepository<Obyekt, MyDbContext>, IObyekt
    {
        public List<Obyekt> GetAll()
            
        {
            using (MyDbContext context = new MyDbContext())
            {
                var result = from rentHome in context.Set<Obyekt>()
                             join imgName in context.Set<ObyektImg>() on rentHome.Id equals imgName.ImgIdForeignId into imgNamesGroup
                             from imgName in imgNamesGroup.DefaultIfEmpty()
                             join customer in context.Set<ObyektSecondStepCustomer>() on rentHome.Id equals customer.SecondStepCustomerForeignId into customersGroup
                             from customer in customersGroup.DefaultIfEmpty()
                             select new Obyekt
                             {
                                 Id = rentHome.Id,
                                 Fullname = rentHome.Fullname,
                                 Number = rentHome.Number,
                                 Region = rentHome.Region,
                                 Address = rentHome.Address,
                                 Metro = rentHome.Metro,
                                 CoordinateX = rentHome.CoordinateX,
                                 CoordinateY = rentHome.CoordinateY,
                                 Room = rentHome.Room,
                                 Repair = rentHome.Repair,
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
                                 ObyektImgs = imgName == null ? null : new List<ObyektImg>
                                 {
                                     new ObyektImg
                                     {
                                         ImgId = imgName.ImgId,
                                         ImgIdForeignId = imgName.ImgIdForeignId,
                                         ImgPath = imgName.ImgPath,
                                     }
                                 }.ToList(),
                                 ObyektSecondStepCustomers = customer == null ? null : new List<ObyektSecondStepCustomer>
                                 {
                                     new ObyektSecondStepCustomer
                                     {
                                         SecondStepCustomerId = customer.SecondStepCustomerId,
                                         SecondStepCustomerForeignId = customer.SecondStepCustomerForeignId,
                                         FullName = customer.FullName,
                                         Number = customer.Number,
                                         DirectCustomerDate = customer.DirectCustomerDate,
                                     }
                                 }.ToList()
                             };

                return result.ToList();
            }
        }
    }
}
