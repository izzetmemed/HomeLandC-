using Core;
using Model.DTOmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGenericGetAllPage
    {
        Task<IDataResult<SearchDTO>> GetAllPage(int Page);
    }
}
