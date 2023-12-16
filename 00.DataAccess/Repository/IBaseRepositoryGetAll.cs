using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepositoryGetAll<T> where T : class
    {
        List<T> GetAll();
    }
}
