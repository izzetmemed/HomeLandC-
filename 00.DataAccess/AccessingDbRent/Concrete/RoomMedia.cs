using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.Repository;
using Model.Models;
namespace DataAccess.AccessingDbRent.Concrete
{
    public class RoomMedia:BaseRepositoryAdd<Room>,IBaseRepositoryAdd<Room>
    {
    }
}
