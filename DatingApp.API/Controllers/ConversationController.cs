using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationRepository _repo;
        private readonly IMapper _mapper;
        public ConversationController(IConversationRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        [Route("GetConversation")]
        public IActionResult GetConversation(string user, string user_counter)
        {
            var conversation = _mapper.Map<List<ConversationDto>>(_repo.GetConversation(user, user_counter));
            return Ok(conversation);
        }
    }
}