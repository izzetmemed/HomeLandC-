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
        public async Task<IResult> Add(ObyektImg Model)
        {

            if (string.IsNullOrWhiteSpace(Model.ImgPath))
            {
                return new ErrorResult(MyMessage.Success);
            }
            else
            {
                AccessImg.Add(Model);
            }

           
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> Delete(ObyektImg Model)
        {
            AccessImg.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await AccessImg.GetAll());
        }

        public async Task<IDataResult<ObyektImg>> GetById(int id)
        {
            return new SuccessDataResult<ObyektImg>(AccessImg.GetById(x => x.ImgId == id));
        }
        public async Task<IDataResult<List<ObyektImg>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<ObyektImg>>(await AccessImg.GetByIdList(id));
        }
        public async Task<IResult> Update(ObyektImg Model)
        {
            AccessImg.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
        public async Task<IResult> DeleteList(int ForeignId)
        {
            AccessImg.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
