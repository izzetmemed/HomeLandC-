using Model.DTOmodels;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepositoryGetAllAdmin<T> where T : class
    {
        Task<SearchDTO> GetAllNormal(int Page, Expression<Func<T, bool>>? predicate = null);
    }
}
