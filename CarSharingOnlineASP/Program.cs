using CarSharing.DB;
using CarSharing.DB.Models;
using CarSharingOnlineASP.Data;
using CarSharingOnlineASP.Models;
using CarSharingOnlineASP.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<ICarsJSRepository, CarsJSRepository>();
//builder.Services.AddSingleton<IUsersJSRepository, UsersJSRepository>();
//builder.Services.AddSingleton<IRentsJSRepository, RentsJSRepository>();
//builder.Services.AddSingleton<IRentService, RentService>();
string connection = builder.Configuration.GetConnectionString("DBCarSharing");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
builder.Services.AddIdentity<UserDB, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();
builder.Services.AddTransient<ICarsDBRepository, CarsDBRepository>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
