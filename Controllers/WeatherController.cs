using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Raimun.Core.Interfaces;
using Raimun.Core.ViewModels;
using Raimun.DataAccessLayer.Entities;
using ThirdParty.Json.LitJson;

namespace Raimun.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class WeatherController : ControllerBase
   {
      private IWeather _weather;
      public WeatherController(IWeather weather)
      {
         _weather = weather;
      }
      public async Task<ActionResult> GetWeather()
      {
         //List<VMWeather> WeatherInfo = new List<VMWeather>();
         double vTemp;
         using (var client = new HttpClient())
         {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=36.564039&lon=53.082845&appid=5caf29042bafd3c1b7198d48745d1247");
            if (Res.IsSuccessStatusCode)
            {
               var ObjResponse = Res.Content.ReadAsStringAsync().Result;
               DataTable obj = (DataTable)JsonConvert.DeserializeObject(ObjResponse, (typeof(DataTable)));
            }
            return Ok();
         }
      }

   }
}