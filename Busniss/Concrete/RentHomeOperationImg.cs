using Business.Abstract;
using Business.Message;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Core;
using DataAccess.AccessingDb.Concrete;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentHomeOperationImg : IRentHomeServiceImg
    {
        AccessImg AccessImg = new AccessImg();
        public IResult Add(ImgName Model)
        {

            AccessImg.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IResult Delete(ImgName Model)
        {
            AccessImg.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<ImgName>> GetAll()
        {
            return new SuccessDataResult<List<ImgName>>(AccessImg.GetAll());
        }

        public IDataResult<ImgName> GetById(int id)
        {
            return new SuccessDataResult<ImgName>(AccessImg.GetById(x => x.ImgId == id));
        }

        public IResult Update(ImgName Model)
        {
            AccessImg.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public IResult DeleteList(int ForeignId)
        {
            AccessImg.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
