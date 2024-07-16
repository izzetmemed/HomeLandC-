using Core;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGenericGetAllPredicate<T>
    {
        Task<IDataResult<List<string>>> GetAll(Expression<Func<T, bool>>? predicate = null);
    }
}
