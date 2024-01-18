using Core;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService<T>
    {
        IResult Add(T Model);
        IDataResult<T> GetByName(string Name);
        IDataResult<List<UserModel>> GetAll();
    }
}
