using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGenericGetAllCoordinate
    {
        Task<IDataResult<List<string>>> GetAllCoordinate();
    }
}
