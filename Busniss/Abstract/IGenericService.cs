using Core;
using Core.BaseModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract 
{
    public interface IGenericService<T> 
    {
        Task<IResult> Add(T Model);
        Task<IResult> Update(T Model);
        Task<IResult> Delete(T Model);
        Task<IDataResult<T>> GetById(int id);
    }
}
