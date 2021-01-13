using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehasan.SimpleChat.API.Model
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public bool IsOnline { get; set; } = false;
        public string Role { get; set; }
    }
}
