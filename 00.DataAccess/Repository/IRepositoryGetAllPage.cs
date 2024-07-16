using Model.DTOmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRepositoryGetAllPage
    {
        Task<SearchDTO> GetAllPage(int Page);
    }
}
