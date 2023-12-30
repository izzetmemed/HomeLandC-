using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaserepositoryGetByİdList<T>
    {
        public Task<List<T>> GetByIdList(int foreignId);
    }
}
