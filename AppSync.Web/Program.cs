using AppSync.Web;
using AppSync.Web.Middlewares;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

//Configure Log4net.
XmlConfigurator.Configure(new FileInfo("log4net.config"));

//Injecting services.
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseMiddleware<ExceptionLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<FirstTimeLoginMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Account}/{action=Login}/{id?}"
);

app.Run();