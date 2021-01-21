using Ehasan.SimpleChat.Core.Entities;
using Ehasan.SimpleChat.Core.Enums;

namespace Ehasan.SimpleChat.Core.Model
{
    public class MessageDeleteModel
    {
        public string DeleteType { get; set; }
        public Message Message { get; set; }
        public string DeletedUserId { get; set; }
    }
}
