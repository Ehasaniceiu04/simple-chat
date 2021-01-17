using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ehasan.SimpleChat.API.Model;
using Ehasan.SimpleChat.Core.AppSetting;
using Ehasan.SimpleChat.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ehasan.SimpleChat.API.Controllers
{
    
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private readonly ApplicationSettings appSettings;

        const string password = "default123@^";

        public AccountController(UserManager<ApplicationUser> userManager, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

       
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = this.userManager.Users.ToList();
            return Ok(result);
        }
       
        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> Register(ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };


            try
            {
                var result = await this.userManager.CreateAsync(applicationUser, password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                try
                {

                    IdentityOptions _options = new IdentityOptions();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim("UserName",user.UserName),
                        new Claim("FullName",user.FirstName+" "+user.LastName),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    user.IsOnline = true;
                    return Ok(new { token, user });
                }
                catch (Exception exp)
                {
                    throw;
                }
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}