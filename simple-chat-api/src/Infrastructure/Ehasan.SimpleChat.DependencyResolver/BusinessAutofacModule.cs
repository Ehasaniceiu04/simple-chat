using Autofac;
using Ehasan.SimpleChat.Business;
using Ehasan.SimpleChat.Business.ServiceQuery;
using Ehasan.SimpleChat.Core.Business_Interface;
using Ehasan.SimpleChat.Core.Business_Interface.ServiceQuery;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.SimpleChat.DependencyResolver
{
    public static class BusinessAutofacModule
    {
        public static ContainerBuilder CreateAutofacBusinessContainer(this IServiceCollection services, ContainerBuilder builder)
        {
            builder.RegisterType<IMessageService>().As<MessageService>();
            builder.RegisterType<IMessageServiceQuery>().As<MessageServiceQuery>();
            return builder;
        }
    }

    public class BusinessAutofacModule1 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<MessageServiceQuery>().As<IMessageServiceQuery>();
        }
    }
}
