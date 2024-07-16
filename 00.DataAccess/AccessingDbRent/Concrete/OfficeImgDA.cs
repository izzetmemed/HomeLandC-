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
    public class OfficeImgDA : BaseRepository<OfficeImg, MyDbContext>, IOfficeImg
    {
        ImgGeneric ImgGeneric { get; set; }
        CustomerGeneric customerGeneric { get; set; }
        public OfficeImgDA()
        {
            ImgGeneric = new ImgGeneric();
            customerGeneric = new CustomerGeneric();
        }
        public void DeleteList(int foreignId)
        {
            customerGeneric.DeleteListGeneric<OfficeImg>(foreignId, "ImgIdForeignId");
        }

        public async Task<List<string>> GetAll()
        {
            return await ImgGeneric.GetAllImgGeneric<OfficeImg>();

        }

        public async Task<List<OfficeImg>> GetByIdList(int foreignId)
        {
            return await customerGeneric.GetByIdListGeneric<OfficeImg>(foreignId, "ImgIdForeignId");
        }
    }
}
