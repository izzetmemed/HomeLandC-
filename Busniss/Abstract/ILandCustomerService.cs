using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    internal interface ILandCustomerService : IGenericService<LandCustomer>,IGenericGetAll ,IGenericServiceDeleteList, IGenericServiceGetByIdList<LandCustomer>
    {
    }
}
