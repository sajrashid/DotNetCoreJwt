using System;
using System.Text;
using DotNetCoreJwt.MiddleWare;
using DotNetCoreJwt.Services.APIKey;
using DotNetCoreJwt.Services.Identity.Claims;
using DotNetCoreJwt.Services.Identity.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace DotNetCoreJwt
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {


                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = false,
                         ClockSkew = TimeSpan.Zero,
                        


                         // JWT Key we can use app.settings
                         //TODO get key from Appsetting for dev
                         //TODO for live we must use and environment variable
                         // Don't forget update the TokenService if you change it here or we will get a key mismatch
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fjboJU3s7rw2Oafzum5fBxZoZ5jihQRbpBZcxZFd/gY="))
                       
                     };
                 });




            // adds app metrics
            services.AddMetrics()
              .AddHealthChecks()
              .AddJsonSerialization()
              .AddMetricsMiddleware(options => options.IgnoredHttpStatusCodes = new[] { 404 });



            // register custom claims service
            services.AddSingleton<IClaimsService, ClaimsService>();



            //register custom Token service
            services.AddSingleton<ITokensService, TokensService>();



            //register custom API Key Generating service
            services.AddSingleton<IApiKeysService, APIKeysService>();



            //add mvc with options for AppMetrics set this back to  services.AddMvc(); if Appmetrics is removed
            services.AddMvc(options => options.AddMetricsResourceFilter());
          



            // swagger needs to be after service.mvc
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
            // url is http://localhost:port/swagger/

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });



            // TODO
            // What does the below do, does it offer any advantages ?
            //services.AddAuthorization(options => { options.DefaultPolicy = new 
            //AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build(); });


        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }




            // set startup file to index.html for easy testing
            app.UseDefaultFiles();

            
            //added support for static file
            // 401's on favicons are no more!
            app.UseStaticFiles();



            // tells api to auth
            app.UseAuthentication();

            // This is the error handling middleWare 
            // this traps error on invoking next
            // This Middleware neeeds to be before other custom middleware to work
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // run the custom auth middleware componenet
            // Rename From Auth MiddleW
            // at the moment it just checks if your autheticated with bearer
            app.UseMiddleware<VerifyTokenSenderMiddleWare>();


            // run the custom auth middleware componenet
            // Rename From Auth MiddleW
            // at the moment it just checks if your autheticated with AD
            app.UseMiddleware<WindowsAuthenticationMiddleWare>();


            //app metrics 
            //https://al-hardy.blog/2017/04/28/asp-net-core-monitoring-with-influxdb-grafana/
            app.UseMetrics();



            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

          

            app.UseMvc();
        }
    }
}
