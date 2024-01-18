using DataAccess.AccessingDbRent.Abstract;
using DataAccess.Repository;
using Model.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
namespace DataAccess.AccessingDbRent.Concrete
{
    public class UserModelAccess : UserBaseRepository<UserModel, MyDbContext>
    {
     
    }
}
