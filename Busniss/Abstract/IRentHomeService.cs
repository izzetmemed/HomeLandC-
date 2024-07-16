using Business.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentHomeService : IGenericService<RentHome>, IGenericGetAllPredicate<RentHome>, IGenericGetAllRecommend, IGenricServiceGetByIdObject, IGenericServiceGetAllNormal, IGenericServiceGetByIdAdmin, IGenericGetAllCoordinate, IGenericGetAllPage, IGenericGetAllSearch,IGetAllCustomerNumber,IGetAllId,IGetAllOwnNumber
    {
    }
}
