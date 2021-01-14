using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ehasan.SimpleChat.Core.Business_Interface;
using Ehasan.SimpleChat.Core.Business_Interface.ServiceQuery;
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
        public IActionResult GetAll()
        {
            var messages = this.messageServiceQuery.GetAll();
            return Ok(messages);
        }
    }
}