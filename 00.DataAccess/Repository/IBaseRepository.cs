using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepository<T> where T : class, new()
    {
        void Add(T Model);
        void Delete(T Model);
        void Update(T Model);
        T GetById(Expression<Func<T, bool>> predicate);
    }
}
