using ERPConnect.Web.Interfaces;
using ERPConnect.Web.Models;
using ERPConnect.Web.Models.Context;
using ERPConnect.Web.Models.Repository;
using ERPConnect.Web.Security;
using ERPConnect.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

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
                //options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("FirstTimePasswordChangePolicy",
                    policy => policy.RequireClaim("FirstTimeLogin", "True"));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin", "True"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("DeleteRole", "True"));

            });

            services.ConfigureApplicationCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(20));

            services.AddControllersWithViews();

            services.AddMvc(options =>
            {
                //To apply[Authorize] attribute globally on all controllers and controller actions throughout our application
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddXmlSerializerFormatters();

            services.AddSingleton<EmailService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.AddScoped<IOTPVerificationRepository, OTPVerificationRepository>();
            services.AddScoped<IMenuServiceRepository, MenuServiceRepository>();
            services.AddScoped<IMasterEntryRepository, MasterEntryRepository>();
        }
    }
}
