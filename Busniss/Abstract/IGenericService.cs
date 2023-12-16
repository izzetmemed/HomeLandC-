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
        IResult Add(T Model);
        IResult Update(T Model);
        IResult Delete(T Model);
        IDataResult<T> GetById(int id);
        IDataResult<List<T>> GetAll();
    }
}
