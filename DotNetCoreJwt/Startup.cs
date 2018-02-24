using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreJwt.MiddleWare;
using DotNetCoreJwt.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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


                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ClockSkew = TimeSpan.Zero,
                         ValidIssuer = "localhost",
                         ValidAudience = "localhost",


                         // JWT Key we can use app.settings
                         //TODO get key from Appsetting for dev
                         //TODO for live we must use and environment variable
                         // Don't forget update the TokenService if you change it here or we will get a key mismatch
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Rather_very_long_key"))
                       
                     };
                 });




            // adds app metrics
            services.AddMetrics()
              .AddHealthChecks()
              .AddJsonSerialization()
              .AddMetricsMiddleware(options => options.IgnoredHttpStatusCodes = new[] { 404 });



            // register custom claims service
            services.AddSingleton<ClaimsService>();



            //register custom Token service
            services.AddSingleton<TokensService>();



            //add mvc with options for AppMetrics set this back to  services.AddMvc(); if Appmetrics is removed
            services.AddMvc(options => options.AddMetricsResourceFilter());
          



            // swagger needs to be after service.mvc
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
            // url is http://localhost:port/swagger/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
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



            //added support for static file
            // 401's on favicons are no more!
            app.UseStaticFiles();




            // tells api to auth
            app.UseAuthentication();




            // run the custom auth middleware componenet
            // We should rename this it's not realy doing auth
            // at the moment it just checks if your autheticated with bearer or windows auth
            // maybe we call is validation middeware as where validating the user is from mule for example
            app.UseMiddleware<Authorize>();




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
