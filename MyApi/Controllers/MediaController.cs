using Business.Concrete;
using DataAccess.AccessingDb.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOmodels;
using Model.Models;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly MediaOperation _mediaOperation;
        private readonly MediaChildOperation mediaChildOperation;
      
        public MediaController()
        {
            _mediaOperation = new MediaOperation();
            mediaChildOperation=new MediaChildOperation();
          
        }

        [HttpPost]
        public async Task<IActionResult> Add(MediaDTO mediaDto)
        {
            if (mediaDto == null)
            {
                return BadRequest("MediaDTO cannot be null.");
            }

            try
            {
                if (mediaDto.mediaType != null)
                {
                    await _mediaOperation.Add(mediaDto.mediaType);
                }
                int id = await GetLastId();
                if (mediaDto.Metro != null && mediaDto.Metro.Count != 0)
                {
                    foreach (var metroName in mediaDto.Metro)
                    {
                        var metro = new Metro
                        {
                            MetroForeignId = id,
                            MetroName = metroName
                        };
                        await mediaChildOperation.AddMetro(metro);
                    }
                }

                if (mediaDto.Room != null && mediaDto.Room.Count != 0)
                {
                    foreach (var roomCount in mediaDto.Room)
                    {
                        var room = new Room
                        {
                            RoomForeignId = id,
                            RoomCount = byte.Parse(roomCount)
                        };
                        await mediaChildOperation.AddRoom(room);
                    }
                }

                if (mediaDto.Region != null && mediaDto.Region.Count !=0)
                {
                    foreach (var regionName in mediaDto.Region)
                    {
                        var region = new Region
                        {
                            RegionForeignId = id,
                            RegionName = regionName
                        };
                        await mediaChildOperation.AddRegion(region);
                    }
                }

                if (mediaDto.Building != null && mediaDto.Building.Count != 0)
                {
                    foreach (var buildingKind in mediaDto.Building)
                    {
                        var building = new Building
                        {
                            BuildingForeignId = id,
                            BuildingKind = buildingKind
                        };
                        await mediaChildOperation.AddBuilding(building);
                    }
                }

                return Ok("Media added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error occurred while adding media: {ex.Message}");
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> Add( MediaType media)
        //{
        //    if (media == null)
        //    {
        //        return BadRequest("Media object is null");
        //    }

        //    try
        //    {
        //        await _mediaOperation.Add(media);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
        //    }
        //}

        //[HttpPost("Metro")]
        //public async Task<IActionResult> AddMetro([FromBody] List<string> Metro)
        //{
        //    if (Metro == null)
        //    {
        //        return BadRequest("Media object is null");
        //    }

        //    try
        //    {
        //        Metro = Metro.Distinct().ToList();
        //        var id = await GetLastId();
        //        Metro.ForEach(x =>
        //        {
        //            Metro metro = new Metro();
        //            metro.MetroForeignId = id;
        //            metro.MetroName = x;
        //            mediaChildOperation.AddMetro(metro);
        //        });
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
        //    }
        //}
        //[HttpPost("Room")]
        //public async Task<IActionResult> AddRoom([FromBody] List<string> Room)
        //{
        //    if (Room == null)
        //    {
        //        return BadRequest("Media object is null");
        //    }

        //    try
        //    {
        //        Room = Room.Distinct().ToList();
        //        var id= await GetLastId();
        //        Room.ForEach(x =>
        //        {
        //            Room room = new Room();
        //            room.RoomForeignId = id;
        //            room.RoomCount = byte.Parse(x);
        //            mediaChildOperation.AddRoom(room);
        //        });
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
        //    }
        //}
        //[HttpPost("Region")]
        //public async Task<IActionResult> AddRegion([FromBody] List<string> Region)
        //{
        //    if (Region == null)
        //    {
        //        return BadRequest("Media object is null");
        //    }

        //    try
        //    {
        //        Region = Region.Distinct().ToList();
        //        var id = await GetLastId();
        //        Region.ForEach(x =>
        //        {
        //            Region region = new Region();
        //            region.RegionForeignId = id;
        //            region.RegionName = x;
        //            mediaChildOperation.AddRegion(region);
        //        });
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
        //    }
        //}
        //[HttpPost("Building")]
        //public async Task<IActionResult> AddBuilding([FromBody]  List<string> Building)
        //{
        //    if (Building == null)
        //    {
        //        return BadRequest("Media object is null");
        //    }

        //    try
        //    {
        //        Building = Building.Distinct().ToList();
        //        var id = await GetLastId();
        //        Building.ForEach(x =>
        //        {
        //            Building building = new Building();
        //            building.BuildingForeignId =id;
        //            building.BuildingKind = x;
        //            mediaChildOperation.AddBuilding(building);
        //        });
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
        //    }
        //}

        private async Task<int> GetLastId()
        {
            var items = await _mediaOperation.GetAll();

            if (items.Any())
            {
                var lastItem = items.LastOrDefault();
                return lastItem.Id;
            }
            else
            {
                return 0;
            }
        }
        
    }
}
