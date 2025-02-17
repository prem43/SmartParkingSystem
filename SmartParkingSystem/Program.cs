using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartParkingSystem.Infrastructure;
using SmartParkingSystem.Models;
using SmartParkingSystem.Repositories;
using SmartParkingSystem.Repositories.IRepositories;
using SmartParkingSystem.Services;
using SmartParkingSystem.Services.IServices;
using SmartParkingSystem.Stores;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure DatabaseHelper as a Singleton Service
builder.Services.AddSingleton<DatabaseHelper>();

// ✅ Register Repositories
builder.Services.AddScoped<IRepository<ApplicationUser>, UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IParkingLotsRepository, ParkingLotsRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

// ✅ Register Services
builder.Services.AddScoped<IService<ApplicationUser>, UserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IParkingLotsService, ParkingLotsService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

// ✅ Add IHttpContextAccessor for authentication handling
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// ✅ Identity Configuration (Without Entity Framework)
builder.Services.AddScoped<IUserStore<ApplicationUser>, UserStore>();
builder.Services.AddScoped<IRoleStore<IdentityRole>, RoleStore>();

// ✅ Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddUserStore<UserStore>()
    .AddRoleStore<RoleStore>()
    .AddDefaultTokenProviders();

// ✅ Configure Authentication using Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

// ✅ Add MVC Support
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Enable error handling in development mode
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ✅ Ensure Static Files are Served Correctly
app.UseStaticFiles();

// ✅ Routing Middleware
app.UseRouting();

// ✅ Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// ✅ Configure Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
