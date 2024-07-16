using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISellService:IGenericService<Sell>, IGenericGetAllPredicate<Sell>, IGenricServiceGetByIdObject, IGenericServiceGetAllNormal, IGenericServiceGetByIdAdmin,IGenericGetAllRecommend, IGenericGetAllCoordinate, IGenericGetAllPage,IGenericGetAllSearch, IGetAllCustomerNumber, IGetAllId, IGetAllOwnNumber
    {
    }
}
