using Business.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        MediaOperation mediaOperation=new MediaOperation();

        [HttpGet]
        public async void Add(Medium media)
        {
            try
            {
                await mediaOperation.Add(media);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }
    }
}
