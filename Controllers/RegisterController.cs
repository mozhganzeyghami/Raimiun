using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Raimun.Core.Interfaces;
using Raimun.DataAccessLayer.Entities;

namespace Raimun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
      private IUser _user;
      public RegisterController(IUser user)
      {
         _user = user;
      }
      [HttpPost]
      public IActionResult Register([FromBody] User user)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest();
         }
         else
         {
            _user.InsertUser(user);
            return CreatedAtAction("Login", new { username = user.UserName, password = user.Password }, user);
         }
      }

   }
}