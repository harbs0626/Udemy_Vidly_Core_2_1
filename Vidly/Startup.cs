﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vidly.Models;
using Vidly.Models.EntityFrameworks;
using Vidly.Models.Interfaces;
using Vidly.App_Start;

namespace Vidly
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:VidlyCore:ConnectionString"]));

            services.AddMvc();

            services.AddTransient<IMembershipTypeRepository, EFMembershipTypeRepository>();
            services.AddTransient<IGenreRepository, EFGenreRepository>();

            //services.AddMemoryCache();
            //services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();

            SeedData.EnsurePopulated(app);

            AutoMapper.Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}
