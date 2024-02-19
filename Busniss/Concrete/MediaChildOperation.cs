using DataAccess.AccessingDbRent.Abstract;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.AccessingDbRent.Concrete;
using Core;
using Business.Message;
namespace Business.Concrete
{
    public class MediaChildOperation : IMediaChildService<Room, Metro, Region, Building>
    {
        public BuildingMedia buildingMedia=new BuildingMedia();
        public MetroMedia metroMedia=new MetroMedia();
        public RegionMedia regionMedia=new RegionMedia();
        public RoomMedia roomMedia=new RoomMedia();
        public async Task<IResult> AddBuilding(Building child)
        {
            buildingMedia.Add(child);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> AddMetro(Metro child)
        {
            metroMedia.Add(child);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> AddRegion(Region child)
        {
            regionMedia.Add(child);
            return new SuccessResult(MyMessage.Success);
        }

        public async Task<IResult> AddRoom(Room child)
        {
           roomMedia.Add(child);
            return new SuccessResult(MyMessage.Success);
        }
    }
}
