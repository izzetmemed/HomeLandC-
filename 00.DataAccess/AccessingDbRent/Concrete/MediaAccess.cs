using DataAccess.AccessingDbRent.Abstract;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessingDbRent.Concrete
{
    public class MediaAccess : BaseRepository<MediaType, MyDbContext>, IBaseRepository<MediaType>,IMedia
    {
        public async Task<List<MediaType>> GetAll()
        {
            using (MyDbContext context = new MyDbContext())
            {
                return await context.Set<MediaType>().ToListAsync();
            }
        }

        public async Task<List<MediaType>> GetAllObyekt(Obyekt data)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Media = await context.Set<MediaType>()
                          .Where(x => x.Type == "Obyekt")
                          .ToListAsync();

                Media = Media.Where(x => int.Parse(x.Counter) > 0)
                             .ToList();


                var Metro = await context.Set<Metro>().ToListAsync();
                var Room = await context.Set<Room>().ToListAsync();
                var Building = await context.Set<Building>().ToListAsync();
                var Region = await context.Set<Region>().ToListAsync();

                if (Media != null)
                {
                    foreach (var rentHome in Media)
                    {
                        rentHome.Metros = Metro.Where(x => x.MetroForeignId == rentHome.Id).ToList();
                        rentHome.Rooms = Room.Where(x => x.RoomForeignId == rentHome.Id).ToList();
                        rentHome.Buildings = Building.Where(x => x.BuildingForeignId == rentHome.Id).ToList();
                        rentHome.Regions = Region.Where(x => x.RegionForeignId == rentHome.Id).ToList();
                    }

                    var suitableData = Media
         .Where(x => (x.Buildings.Count == 0 ? true : x.Buildings.Any(x => x.BuildingKind == data.SellOrRent))
                && (x.Metros.Count == 0 ? true : x.Metros.Any(x => x.MetroName == data.Metro))
                && (x.Rooms.Count == 0 || x.Rooms.Any(r => r.RoomCount == data.Room))
                  && (x.Regions.Count == 0 ? true : x.Regions.Any(x => x.RegionName == data.Region))
                   && (data.Price < x.MaxPrice)
                    && (data.Price > x.MinPrice)
                 )
         .ToList();


                    return suitableData;
                }
                else
                {
                    return new List<MediaType>();
                }

            }
        }

        public async Task<List<MediaType>> GetAllRent(RentHome data)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Media = await context.Set<MediaType>()
                          .Where(x => x.Type == "Rent")
                          .ToListAsync(); 

                Media = Media.Where(x => int.Parse(x.Counter) > 0)
                             .ToList();


                var Metro = await context.Set<Metro>().ToListAsync();
                var Room = await context.Set<Room>().ToListAsync();
                var Building = await context.Set<Building>().ToListAsync();
                var Region = await context.Set<Region>().ToListAsync();

                if (Media != null)
                {
                    foreach (var rentHome in Media)
                    {
                        rentHome.Metros = Metro.Where(x => x.MetroForeignId == rentHome.Id).ToList();
                        rentHome.Rooms = Room.Where(x => x.RoomForeignId == rentHome.Id).ToList();
                        rentHome.Buildings = Building.Where(x => x.BuildingForeignId == rentHome.Id).ToList();
                        rentHome.Regions = Region.Where(x => x.RegionForeignId == rentHome.Id).ToList();
                    }

                    var suitableData = Media
         .Where(x => (x.Buildings.Count == 0 ? true : x.Buildings.Any(x => x.BuildingKind == data.Building))
                && (x.Metros.Count == 0 ? true : x.Metros.Any(x => x.MetroName == data.Metro))
                && (x.Rooms.Count == 0 || x.Rooms.Any(r => r.RoomCount == data.Room))
                  && (x.Regions.Count == 0 ? true : x.Regions.Any(x => x.RegionName == data.Region))
                   && (data.Price < x.MaxPrice)
                    && (data.Price > x.MinPrice)
                 )
         .ToList();


                    return suitableData;
                }
                else
                {
                   return new List<MediaType>();
                }
               
            }
        }

        public async Task<List<MediaType>> GetAllSell(Sell data)
        {
            using (MyDbContext context = new MyDbContext())
            {
                var Media = await context.Set<MediaType>()
                          .Where(x => x.Type == "Sell")
                          .ToListAsync();

                Media = Media.Where(x => int.Parse(x.Counter) > 0)
                             .ToList();


                var Metro = await context.Set<Metro>().ToListAsync();
                var Room = await context.Set<Room>().ToListAsync();
                var Building = await context.Set<Building>().ToListAsync();
                var Region = await context.Set<Region>().ToListAsync();

                if (Media != null)
                {
                    foreach (var rentHome in Media)
                    {
                        rentHome.Metros = Metro.Where(x => x.MetroForeignId == rentHome.Id).ToList();
                        rentHome.Rooms = Room.Where(x => x.RoomForeignId == rentHome.Id).ToList();
                        rentHome.Buildings = Building.Where(x => x.BuildingForeignId == rentHome.Id).ToList();
                        rentHome.Regions = Region.Where(x => x.RegionForeignId == rentHome.Id).ToList();
                    }

                    var suitableData = Media
         .Where(x => (x.Buildings.Count==0 ? true : x.Buildings.Any(x => x.BuildingKind == data.Building))
                && (x.Metros.Count == 0 ? true : x.Metros.Any(x => x.MetroName == data.Metro))
                && (x.Rooms.Count == 0 || x.Rooms.Any(r => r.RoomCount == data.Room))
                  && (x.Regions.Count == 0 ? true : x.Regions.Any(x => x.RegionName == data.Region))
                   && (data.Price < x.MaxPrice)
                    && (data.Price > x.MinPrice)
                 )
         .ToList();


                    return suitableData;
                }
                else
                {
                    return new List<MediaType>();
                }

            }
        }
    }
}
