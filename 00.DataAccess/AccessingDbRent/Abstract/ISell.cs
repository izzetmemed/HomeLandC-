using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00.DataAccess.AccessingDbRent.Abstract
{
    public interface ISell : IBaseRepository<Sell>, IBaseRepositoryGetAll<Sell>
    {
    }
}
