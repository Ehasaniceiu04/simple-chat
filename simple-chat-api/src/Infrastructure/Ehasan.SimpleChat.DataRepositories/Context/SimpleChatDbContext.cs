using Ehasan.SimpleChat.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.SimpleChat.DataRepositories.Context
{
    public class SimpleChatDbContext: IdentityDbContext
    {
        public SimpleChatDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
