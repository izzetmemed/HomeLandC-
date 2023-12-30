using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IBaseRepositoryGetById
    {
        Task<List<ImgName>> GetByIdList(int foriegnId);
    }
}
