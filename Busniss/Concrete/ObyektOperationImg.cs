using Business.Abstract;
using Business.Message;
using Core;
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
    public class ObyektOperationImg : IObyektServiceImg
    {
        public ObyektAccessImg AccessImg { get; set; }
        public ObyektOperationImg()
        {
            AccessImg = new ObyektAccessImg();
        }
        public IResult Add(ObyektImg Model)
        {
            AccessImg.Add(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IResult Delete(ObyektImg Model)
        {
            AccessImg.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public IDataResult<List<ObyektImg>> GetAll()
        {
            return new SuccessDataResult<List<ObyektImg>>(AccessImg.GetAll()); 
        }

        public IDataResult<ObyektImg> GetById(int id)
        {
            return new SuccessDataResult<ObyektImg>(AccessImg.GetById(x => x.ImgId == id));
        }

        public IResult Update(ObyektImg Model)
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
