using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    internal interface ILandImgService : IGenericService<LandImg>, IGenericGetAll, IGenericServiceGetByIdList<LandImg>, IGenericServiceDeleteList
    {
    }
}
