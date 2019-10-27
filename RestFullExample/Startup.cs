using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RestFullExample.Contracts;
using RestFullExample.Model.Configuration;
using RestFullExample.Services;

namespace RestFullExample
{
    public class Startup
    {
        private IConfiguration configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UsersService>();
            services.Configure<FirebaseConfig>(configuration.GetSection("firebase"));

            //Cookie
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x=> {
            //        x.LoginPath = "/api/Login";
            //        x.AccessDeniedPath = "/api/UnAutorize";
            //        x.Cookie.Name = "MyNiceCookie";
            //    });

            var secretKey = configuration.GetValue<string>("auth:SecretKey");
            services.Configure<AuthDetails>(configuration.GetSection("auth"));
            var secretKeyByteArray = Encoding.ASCII.GetBytes(secretKey);
            services.AddAuthentication("JwtDefaults")
                .AddJwtBearer("JwtDefaults", x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "Nice",
                        ValidAudience = "Mamash nice",
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyByteArray),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true
                    };

                });

            services.AddAuthorization(x=>x.AddPolicy("Category1", config => {
                config.RequireClaim(ClaimTypes.Name);
                config.RequireClaim("Cateogory", "C1");
            }));

            services.AddControllers()
            .ConfigureApiBehaviorOptions(x =>
            {
                //custom error response
                x.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(actionContext.ModelState) ;
                };

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
            //Who?
            app.UseAuthentication();
            
            // What?
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
