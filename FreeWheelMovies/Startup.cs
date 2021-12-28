using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FreeWheelMovies.Business;
using FreeWheelMovies.Business.Interfaces;
using FreeWheelMovies.Data;
using FreeWheelMovies.Data.DataManager;
using FreeWheelMovies.Data.DataManager.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FreeWheelMovies
{
    public class Startup
    {
        private const string ApiName = "FreeWheelMovie";
        private const string ConnectionStringName = "FreeWheelDatabase";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString(ConnectionStringName);
            services.AddDbContext<FreeWheelMovieDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<FreeWheelUserDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<FreeWheelMovieRatingDbContext>(options => options.UseSqlServer(connectionString));

            //Data Managers
            services.AddScoped<IUserDataManager, UserDataManager>();
            services.AddScoped<IMovieDataManager, MovieDataManager>();
            services.AddScoped<IMovieRatingDataManager, MovieRatingDataManager>();

            //Services
            services.AddScoped<IMovieRatingService,MovieRatingService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ApiName, Version = "v1" });
                c.UseInlineDefinitionsForEnums();
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                xmlFile = "FreeWheelMovies.API.xml";
                xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName); });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseHsts();
            //}

            app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
