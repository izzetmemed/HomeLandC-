using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
namespace DataAccess.AccessingDbRent.Abstract
{
    public interface IMediaChild<T> : IBaseRepositoryAdd<T> where T : class
    {
    }
}
