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
        public async Task<IResult> Add(ImgName Model)
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

        public async Task<IResult> Delete(ImgName Model)
        {
            AccessImg.Delete(Model);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IDataResult<List<string>>> GetAll()
        {
            return new SuccessDataResult<List<string>>(await AccessImg.GetAll());
        }

        public async Task<IDataResult<ImgName>> GetById(int id)
        {
            return new SuccessDataResult<ImgName>(AccessImg.GetById(x => x.ImgId== id));
        }
        public async Task<IDataResult<List<ImgName>>> GetByIdList(int id)
        {
            return new SuccessDataResult<List<ImgName>>(await AccessImg.GetByIdList(id));
        }
        public async Task<IResult> Update(ImgName Model)
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
