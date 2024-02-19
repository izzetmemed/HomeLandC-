using Business.Concrete;
using DataAccess.AccessingDb.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
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
        public async Task<IActionResult> Add( MediaType media)
        {
            if (media == null)
            {
                return BadRequest("Media object is null");
            }

            try
            {
                await _mediaOperation.Add(media);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
            }
        }

        [HttpPost("Metro")]
        public async Task<IActionResult> AddMetro([FromBody] string Metro)
        {
            if (Metro == null)
            {
                return BadRequest("Media object is null");
            }

            try
            {
                Metro metro = new Metro();
                metro.MetroForeignId = await GetLastId();
                metro.MetroName = Metro;
                await mediaChildOperation.AddMetro(metro);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
            }
        }
        [HttpPost("Room")]
        public async Task<IActionResult> AddRoom([FromBody] string Room)
        {
            if (Room == null)
            {
                return BadRequest("Media object is null");
            }

            try
            {
                Room room = new Room();
                room.RoomForeignId = await GetLastId();
                room.RoomCount = byte.Parse(Room);
                await mediaChildOperation.AddRoom(room);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
            }
        }
        [HttpPost("Region")]
        public async Task<IActionResult> AddRegion([FromBody] string Region)
        {
            if (Region == null)
            {
                return BadRequest("Media object is null");
            }

            try
            {
                Region region = new Region();
                region.RegionForeignId = await GetLastId();
                region.RegionName = Region; 
                await mediaChildOperation.AddRegion(region);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
            }
        }
        [HttpPost("Building")]
        public async Task<IActionResult> AddBuilding([FromBody] string Building)
        {
            if (Building == null)
            {
                return BadRequest("Media object is null");
            }

            try
            {
                Building building = new Building();
                building.BuildingForeignId = await GetLastId();
                building.BuildingKind = Building;
                await mediaChildOperation.AddBuilding(building);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred while adding media");
            }
        }

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
