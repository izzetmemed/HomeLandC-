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
    public class OfficeImgOperation :IOfficeImgService
    {
        OfficeImgDA AccessImg ;
        public OfficeImgOperation()
        {
            AccessImg = new OfficeImgDA();
        }
        public async Task<IResult> Add(OfficeImg Model)
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

        public async Task<IResult> Delete(OfficeImg Model)
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

        public async Task<IDataResult<OfficeImg>> GetById(int id)
        {
            return new SuccessDataResult<OfficeImg>(AccessImg.GetById(x => x.ImgId == id));
        }

        public async Task<IDataResult<List<OfficeImg>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<OfficeImg>>(await AccessImg.GetByIdList(id));
        }

        public async Task<IResult> Update(OfficeImg Model)
        {
            AccessImg.Update(Model);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
