using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Repository;
using ERPConnect.Web.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERPConnect.Web
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppDbContext>();

            //services.AddMvc(options =>
            //{
            //    //To apply[Authorize] attribute globally on all controllers and controller actions throughout our application
            //    var policy = new AuthorizationPolicyBuilder()
            //                    .RequireAuthenticatedUser()
            //                    .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));

            //}).AddXmlSerializerFormatters();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("FirstTimePasswordChangePolicy",
                    policy => policy.RequireClaim("FirstTimeLogin", "True"));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin","True"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("DeleteRole","True"));

            });

            services.AddControllersWithViews();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMenuServiceRepository, MenuServiceRepository>();
            services.AddScoped<IMasterEntryRepository, MasterEntryRepository>();

            services.AddScoped<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
        }
    }
}
