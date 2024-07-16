using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentHomeServiceCustomer : IGenericService<SecondStepCustomer>, IGenericGetAll,IGenericServiceDeleteList, IGenericServiceGetByIdList<SecondStepCustomer>
    {
    }
}
