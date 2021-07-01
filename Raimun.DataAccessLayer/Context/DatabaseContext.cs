using Microsoft.EntityFrameworkCore;
using Raimun.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Raimun.DataAccessLayer.Context
{
   public class DatabaseContext : DbContext
   {
      public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
      {

      }

      public DbSet<Weather> Weathers { get; set; }
      public DbSet<User> Users { get; set; }

   }
}
