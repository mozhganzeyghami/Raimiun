using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Raimun.Core.Interfaces;
using Raimun.Core.ViewModels;
using Raimun.DataAccessLayer.Entities;

namespace Raimun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
      private IUser _user;
      public UserController(IUser user)
      {
         _user = user;
      }


      [HttpGet]
      public IActionResult GetAllUser()
      {

         var result = new ObjectResult(_user.GetAllUsers())
         {
            StatusCode = (int)HttpStatusCode.OK
         };
         Request.HttpContext.Response.Headers.Add("X-Count", _user.GetAllUsers().ToString());
         Request.HttpContext.Response.Headers.Add("X-Name", "Mozhgan Zeyghami");

         return result;
      }


      [HttpPost]
      public IActionResult Login([FromBody] User login)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest("The Model Is Not Valid");
         }
         else
         {
            bool IsExistUser = _user.LoginUser(login.UserName, login.Password);
            if (IsExistUser)
            {
               var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RaimunVerifyAppKey"));
               var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
               var tokenOption = new JwtSecurityToken(
                  issuer: "http://localhost:17916",
                  claims: new List<Claim>
                  {
                     new Claim(ClaimTypes.Name, login.UserName),
                     new Claim(ClaimTypes.Role, "Admin"),
                  },
                  expires: DateTime.Now.AddMinutes(30),
                  signingCredentials: signingCredentials
                  );

               var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

               return Ok(new {token = tokenString});
            }
            else
            {
               return Unauthorized();
            }
         }        
      }

   }
}
