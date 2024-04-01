using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserLoginRegister.Contexts;
using UserLoginRegister.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProniaDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<appUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.AllowedForNewUsers = true;

})
    .AddEntityFrameworkStores<ProniaDbContext>();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=Home}/{Action=Index}/{id?}"
    );


app.Run();
