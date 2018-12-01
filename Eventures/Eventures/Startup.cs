using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventures.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventures.Models;
using Eventures.Utilities;
using Eventures.Services.Contracts;
using Eventures.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using AutoMapper;
using Eventures.ViewModels;

namespace Eventures
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            });

            //Update Context
            services.AddDbContext<EventuresDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<EventuresDbContext>();

            services.AddIdentity<EventuresUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<EventuresDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
                options.LogoutPath = "/Accounts/Logout";
                options.AccessDeniedPath = "/Accounts/AccessDenied";
            });

            services.AddScoped<IErrorExtractor, ErrorExtractor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddFacebook(fbOptions =>
            {
                fbOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                fbOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            })
            .AddCookie();

            services.AddAutoMapper(config =>
            {
                config.CreateMap<RegisterBindingModel, EventuresUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
                config.CreateMap<CreateEventBindingModel, EventuresEvent>();
                config.CreateMap<EventuresEvent, BaseEventViewModel>();
                config.CreateMap<EventuresOrder, BaseMyEventViewModel>()
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.TicketsCount));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            RoleSeeder.Seed(provider);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "areaRoute",
                //    template:"{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
