using Ehasan.SimpleChat.Core.Business_Interface;
using Ehasan.SimpleChat.Core.Entities;
using Ehasan.SimpleChat.Core.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.SimpleChat.Business
{
    public class MessageService: IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Add(Message message)
        {
            this.unitOfWork.Repository<Message>().Add(message);
            this.unitOfWork.SaveChanges();
        }
    }
}
