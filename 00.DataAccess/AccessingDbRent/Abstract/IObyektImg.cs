using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Abstract
{
    public interface IObyektImg : IBaseRepository<ObyektImg>, IBaseRepositoryGetAll<ObyektImg>, IBaseRepositoryDeleteList, IBaserepositoryGetByİdList<ObyektImg>
    {
    }
}
