using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ehasan.SimpleChat.Core.Business_Interface;
using Ehasan.SimpleChat.Core.Business_Interface.ServiceQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ehasan.SimpleChat.API.Controllers
{

    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        
        private readonly IMessageServiceQuery messageServiceQuery;
        public MessageController(IMessageServiceQuery messageServiceQuery)
        {
            this.messageServiceQuery = messageServiceQuery;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = this.messageServiceQuery.GetAll();
            return Ok(messages);
        }
        
        
        [HttpGet("received-messages/{userId}")]
        public IActionResult GetUserReceivedMessages(string userId)
        {
            var messages = this.messageServiceQuery.GetReceivedMessages(userId);
            return Ok(messages);
        }
    }
}