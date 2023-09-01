

using ERPConnect.Web.Extensions;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Load app settings from appsettings.json or other configuration sources.
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

//Configure Log4net.
XmlConfigurator.Configure(new FileInfo("log4net.config"));

//Injecting services.
builder.Services.RegisterServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseLoggerMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{Controller=Account}/{action=Login}/{id?}"
    );
});

app.Run();