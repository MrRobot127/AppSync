using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERPConnect.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddControllersWithViews();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMenuServiceRepository, MenuServiceRepository>();
            services.AddScoped<IMasterEntryRepository, MasterEntryRepository>();


        }
    }
}
