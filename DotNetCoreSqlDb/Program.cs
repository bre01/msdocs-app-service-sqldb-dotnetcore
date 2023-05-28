using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetCoreSqlDb.Data;
var builder = WebApplication.CreateBuilder(args);

// Add database context and cache
builder.Services.AddDbContext<MyDatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=msdocs-core-sql-fir-server.database.windows.net,1433;Initial Catalog=msdocs-core-sql-fir-database;User ID=msdocs-core-sql-fir-server-admin;Password=43ITNLU84UZI8FVL$")));
builder.Services.AddStackExchangeRedisCache(options =>
{
options.Configuration = builder.Configuration["msdocs-core-sql-fir-cache.redis.cache.windows.net:6380,password=ALn6EDcoe3dMnb1nJ56Ar0kRhrF77e3oFAzCaDcFvIA=,ssl=True,defaultDatabase=0"];
options.InstanceName = "SampleInstance";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add App Service logging
builder.Logging.AddAzureWebAppDiagnostics();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
