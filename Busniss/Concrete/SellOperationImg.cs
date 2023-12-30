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
            sellAccessImg = new SellAccessImg();
        }
        public async Task<IResult> Add(SellImg Model)
        {
            if (string.IsNullOrWhiteSpace(Model.ImgPath))
            {
                  return new ErrorResult(MyMessage.Success);
            }
            else
            {
                sellAccessImg.Add(Model);
            }
           
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> Delete(SellImg Model)
        {
            sellAccessImg.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await sellAccessImg.GetAll());
        }

        public async Task<IDataResult<SellImg>> GetById(int id)
        {
            return new SuccessDataResult<SellImg>(sellAccessImg.GetById(x => x.ImgIdForeignId == id));
        }

        public async Task<IResult> Update(SellImg Model)
        {
            sellAccessImg.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public async Task<IResult> DeleteList(int ForeignId)
        {
            sellAccessImg.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
