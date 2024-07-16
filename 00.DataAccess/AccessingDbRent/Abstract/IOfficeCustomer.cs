﻿using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Abstract
{
    internal interface IOfficeCustomer : IBaseRepository<OfficeCustomer>, IBaseRepositoryGetAll<OfficeCustomer>, IBaseRepositoryDeleteList, IBaserepositoryGetByİdList<OfficeCustomer>
    {
    }
}
