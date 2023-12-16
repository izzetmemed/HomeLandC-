using Business.Abstract;
using Business.Message;
using Core;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{ 
    public class SellOperationImg : ISellServiceImg
    {
        public SellAccessImg sellAccessImg { get; set; }

        public SellOperationImg()
        {
            sellAccessImg=new SellAccessImg();
        }
        public IResult Add(SellImg Model)
    {
            if (string.IsNullOrWhiteSpace(Model.ImgPath))
            {
                return new ErrorResult(MyMessage.Success);
            }
            sellAccessImg.Add(Model);
        return new SuccessResult(MyMessage.Success);
    }

    public IResult Delete(SellImg Model)
    {
         sellAccessImg.Delete(Model);
         return new SuccessResult(MyMessage.Success);    
    }

    public IDataResult<List<SellImg>> GetAll()
    {
        return new SuccessDataResult<List<SellImg>>(sellAccessImg.GetAll());
    }

    public IDataResult<SellImg> GetById(int id)
    {
            return new SuccessDataResult<SellImg>(sellAccessImg.GetById(x => x.ImgId == id));
    }

    public IResult Update(SellImg Model)
    {
      sellAccessImg.Update(Model);
            return new SuccessResult(MyMessage.Success);
    }
}
}
