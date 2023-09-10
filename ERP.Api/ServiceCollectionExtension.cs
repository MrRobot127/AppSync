using ERP.Api.Interface;
using ERP.Api.Models;
using ERP.Api.Models.Context;
using ERP.Api.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace ERP.Api
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection")));
            services.AddControllers();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMasterEntryRepository, MasterEntryRepository>();
        }
    }
}
