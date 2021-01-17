using Ehasan.SimpleChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.SimpleChat.Core.Business_Interface
{
    public interface IMessageService
    {
        void Add(Message message);
    }
}
