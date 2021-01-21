using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ehasan.SimpleChat.Core.Business_Interface;
using Ehasan.SimpleChat.Core.Business_Interface.ServiceQuery;
using Ehasan.SimpleChat.Core.Entities;
using Ehasan.SimpleChat.Core.Model;
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
        private readonly IMessageService messageService;
        public MessageController(IMessageServiceQuery messageServiceQuery,IMessageService messageService)
        {
            this.messageServiceQuery = messageServiceQuery;
            this.messageService = messageService;
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
        [HttpPost()]
        public async Task<IActionResult> DeleteMessage([FromBody]MessageDeleteModel messageDeleteModel)
        {
            var message=await this.messageService.DeleteMessage(messageDeleteModel);
            return Ok(message);
        }
    }
}