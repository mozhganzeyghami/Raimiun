using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Raimun.Core.Interfaces;
using Raimun.Core.Services;
using Raimun.DataAccessLayer.Context;

namespace Raimun
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();

         services.AddDbContext<DatabaseContext>(options =>
         {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
         });
         services.AddTransient<IUser, UserService>();
         services.AddTransient<IWeather, WeatherService>();

         //JWT
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                  ValidateIssuer = true,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = "http://localhost:17916",
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RaimunVerifyAppKey"))
               };
            });
         services.AddCors(options =>
         {
            options.AddPolicy("CorsPolicy", builder =>
            {
               builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .Build();
            });
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseRouting();

         app.UseAuthorization();

         app.UseCors("CorsPolicy");

         app.UseAuthentication();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
