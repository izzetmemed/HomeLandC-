using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMediaChildService<T, T1, T2, T3> where T : class where T1 : class where T2 : class where T3 : class 
    {
        public Task<IResult> AddRoom(T child);
        public Task<IResult> AddMetro(T1 child);
        public Task<IResult> AddRegion(T2 child);
        public Task<IResult> AddBuilding(T3 child);
    }
}
