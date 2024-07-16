﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    internal interface IOfficeService : IGenericService<Office>, IGenericGetAllPredicate<Office>, IGenericGetAllRecommend, IGenricServiceGetByIdObject, IGenericServiceGetAllNormal, IGenericServiceGetByIdAdmin, IGenericGetAllCoordinate, IGenericGetAllPage, IGenericGetAllSearch, IGetAllCustomerNumber, IGetAllId, IGetAllOwnNumber
    {
    }
}