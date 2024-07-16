using _00.DataAccess.AccessingDb.Abstract;
using DataAccess.AccessingDbRent.Abstract;
using DataAccess.AccessingDbRent.Concrete.Generic;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class AccessImg : BaseRepository<ImgName, MyDbContext>, IAccessImg
    {
        ImgGeneric ImgGeneric { get; set; }
        CustomerGeneric customerGeneric { get; set; }
        public AccessImg()
        {
            ImgGeneric = new ImgGeneric();
            customerGeneric= new CustomerGeneric();
        }
        public async void DeleteList(int foreignId)
        {
            customerGeneric.DeleteListGeneric<ImgName>(foreignId, "ImgIdForeignId");
        }

        public async Task<List<string>> GetAll()
        {
           return await ImgGeneric.GetAllImgGeneric<ImgName>();
        }

        public async Task<List<ImgName>> GetByIdList(int foreignId)
        {
           return await customerGeneric.GetByIdListGeneric<ImgName>(foreignId, "ImgIdForeignId");
        }

    }
}