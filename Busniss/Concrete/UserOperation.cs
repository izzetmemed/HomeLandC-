using Business.Abstract;
using Core;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOpration : IUserService<UserModel>
    {
        public UserModelAccess UserAccess =new UserModelAccess();
        public IResult Add(UserModel Model)
        {
            UserAccess.Add(Model);
            return new SuccessResult();
        }

        public IDataResult<List<UserModel>> GetAll()
        {
            return new SuccessDataResult<List<UserModel>>( UserAccess.GetAll());

        }

        public IDataResult<UserModel> GetByName(string Name)
        {
            return new SuccessDataResult<UserModel>(UserAccess.GetById(x => x.UserName == Name ));
        }
    }
}
