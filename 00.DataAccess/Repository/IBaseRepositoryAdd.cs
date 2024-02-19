using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepositoryAdd<T> where T : class
    {
        public void Add(T entity);
    }
}
