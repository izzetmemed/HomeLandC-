using Core;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGenericServiceGetByIdList<T> where T : class
    {
        Task<IDataResult<List<T>>> GetByIdList(int id);
    }
}
