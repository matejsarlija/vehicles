using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VehicleContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("VehicleContext") ?? throw new InvalidOperationException("Connection string 'VehicleContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
