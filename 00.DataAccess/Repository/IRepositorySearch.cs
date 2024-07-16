using Model.DTOmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRepositorySearch
    {
        Task<SearchDTO> GetAllSearch(SearchModel searchModel,int Page);
    }
}
