using Raimun.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raimun.Core.Interfaces
{
   public interface IWeather
   {
      void InsertWeatherInfo(Weather weather);
   }
}
