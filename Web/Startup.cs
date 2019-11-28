using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Web.Areas.Identity.Pages;
using Data.Data;
using Microsoft.AspNetCore.Mvc.Authorization;
using Data.Handlers;

namespace Web
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
            DbName dbName = new DbName();

            services.AddDbContext<Data.DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(dbName.ConnectionName)));
            //services.AddDefaultIdentity<MonitorUser>(options => options.SignIn.RequireConfirmedAccount = true);
            //.AddEntityFrameworkStores<Data.DbContext, Guid>();

            services.AddIdentity<MonitorUser, MonitorRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Data.DbContext>()
                    .AddDefaultTokenProviders()
                    .AddUserStore<UserStore<MonitorUser, MonitorRole, Data.DbContext, Guid>>()
                    .AddRoleStore<RoleStore<MonitorRole, Data.DbContext, Guid>>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);

                options.LoginPath = "/Identity/Account/Login";
                options.SlidingExpiration = true;
            });
            //Allows the injection of the handler to Dashboardsettingsview
            services.AddTransient<DashboardTypeHandler>();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddSingleton<IEmailSender, EmailSender>();

            //Custom settings for identity client-side validation
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Data.DbContext context)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json");

#if !DEBUG
            context.Database.Migrate();
#endif
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Dashboard/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
