using System;
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
using Microsoft.AspNetCore.Identity;

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
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["Data:VidlyCore_Identity:ConnectionString"]));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddTransient<IMembershipTypeRepository, EFMembershipTypeRepository>();
            services.AddTransient<IGenreRepository, EFGenreRepository>();

            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvcWithDefaultRoute();

            SeedData.EnsurePopulated(app);
            SeedData_Identity.EnsurePopulated(app);

            AutoMapper.Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}
