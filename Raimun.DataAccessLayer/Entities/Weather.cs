using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Raimun.DataAccessLayer.Entities
{
   public class Weather
   {
      [Key]
      public int Id { get; set; }
       public string CityName { get; set; }
       public string CityTmp { get; set; }

   }
}
