using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private NewsStore _newsStore;
 
        public NewsController(NewsStore newsStore)
        {
            _newsStore = newsStore;
        }

        [HttpGet]
        public IActionResult GetAllGroups()
        {
            return Ok(_newsStore.GetAllGroups());
        }
 
        [HttpPost]
        public IActionResult AddGroup([FromQuery] string group)
        {
            if (string.IsNullOrEmpty(group))
            {
                return BadRequest();
            }
            _newsStore.AddGroup(group);
            return Created("AddGroup", group);
        }
 
    }
}