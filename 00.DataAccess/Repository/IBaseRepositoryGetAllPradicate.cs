using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepositoryGetAllPradicate<T> where T : class
    {
        Task<List<string>> GetAll(Expression<Func<T, bool>>? predicate = null);
    }
}
