using CarValueApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;

namespace CarValueApi
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
            services.AddCors(options =>
            {
               options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });

            services.AddControllers();
            //services.AddScoped<ICarValueAppRepo, MockCarValueAppRepo>();
            // using jwt authentication to generate token
            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = ConfigurationManager.AppSettings["Issuer"],
                    ValidAudience = ConfigurationManager.AppSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddMvc();
            
            //connecting the CarValueApp repo to SqlCarValueApp repo
            services.AddScoped<ICarValueAppRepo, SqlCarValueAppRepo>();
            //using entity framework to link to the database
            services.AddDbContext<CarValueAppContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("CarValueAppConnection")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //using automapper
            // using swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Swagger Demo",
                    Description = "Swagger Demo for ValuesController",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Oladunni",
                        Email = "timilehinayomipo@yahoo.com",
                        Url = "www.google.com"
                    }
                });

                //Locate xml file being generated by asp.net
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory , xmlFile);
                // telling swagger to use those comments
                c.IncludeXmlComments(xmlPath);
            });
            
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
           
            app.UseAuthorization();
            // linking the startup to swagger documantation
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            
        }
    }
}
