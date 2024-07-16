using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentHomeServiceImg : IGenericService<ImgName>, IGenericGetAll,IGenericServiceGetByIdList<ImgName>,IGenericServiceDeleteList
    {
    }
}
