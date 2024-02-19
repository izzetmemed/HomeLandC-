using DataAccess.Repository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Abstract
{
    internal interface IMedia
    {
        Task<List<MediaType>> GetAll();
        Task<List<MediaType>> GetAllRent(RentHome data);
        Task<List<MediaType>> GetAllSell(Sell data);
        Task<List<MediaType>> GetAllObyekt(Obyekt data);
    }
}
