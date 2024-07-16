using Business.Abstract;
using Business.Message;
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
    public class LandImgOperation : ILandImgService
    {
        LandImgDA AccessImg;
        public LandImgOperation()
        {
            AccessImg = new LandImgDA();
        }
        public async Task<IResult> Add(LandImg Model)
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

        public async Task<IResult> Delete(LandImg Model)
        {
            AccessImg.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> DeleteList(int ForeignId)
        {
            AccessImg.DeleteList(ForeignId);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await AccessImg.GetAll());
        }

        public async Task<IDataResult<LandImg>> GetById(int id)
        {
            return new SuccessDataResult<LandImg>(AccessImg.GetById(x => x.ImgId == id));
        }

        public async Task<IDataResult<List<LandImg>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<LandImg>>(await AccessImg.GetByIdList(id));
        }

        public async Task<IResult> Update(LandImg Model)
        {
            AccessImg.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
