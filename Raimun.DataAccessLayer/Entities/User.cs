using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Raimun.DataAccessLayer.Entities
{
   public class User
   {
      [Key]
      public int Id { get; set; }
      [Required(ErrorMessage = "Enter User Name")]
      public string UserName { get; set; }
      [Required(ErrorMessage = "Enter Password")]
      public string Password { get; set; }
      [StringLength(50)]
      public string FName { get; set; }
      [StringLength(50)]
      public string LName { get; set; }

   }
}
