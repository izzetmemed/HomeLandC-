using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IObyektService : IGenericService<Obyekt>, IGenericGetAllPredicate<Obyekt>, IGenericGetAllRecommend, IGenricServiceGetByIdObject,IGenericServiceGetAllNormal, IGenericServiceGetByIdAdmin, IGenericGetAllCoordinate, IGenericGetAllPage,IGenericGetAllSearch, IGetAllCustomerNumber, IGetAllId, IGetAllOwnNumber
    {
    }
}
