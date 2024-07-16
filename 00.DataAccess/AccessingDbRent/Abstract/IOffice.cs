using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Abstract
{
    internal interface IOffice : IBaseRepository<Office>, IBaseRepositoryGetAllPradicate<Office>, IBaseRebositoryGetAllRecommend, IBaseRepositoryGetAllAdmin<Office>, IBaseRepositoryGetAllCoordinate, IRepositoryGetAllPage, IRepositorySearch
    {
    }
}
