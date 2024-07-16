using Core;
using Core.BaseModel;
using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00.DataAccess.AccessingDb.Abstract
{
    public interface IAccess : IBaseRepository<RentHome>, IBaseRepositoryGetAllPradicate<RentHome>, IBaseRebositoryGetAllRecommend,IBaseRepositoryGetAllAdmin<RentHome>, IBaseRepositoryGetAllCoordinate,IRepositoryGetAllPage,IRepositorySearch
    {

    }
}
