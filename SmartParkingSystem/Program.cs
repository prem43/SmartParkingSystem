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

// ✅ Add Repository and Service Layer
builder.Services.AddScoped<IRepository<ApplicationUser>, UserRepository>();
builder.Services.AddScoped<IParkingLotsRepository, ParkingLotsRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IService<ApplicationUser>, UserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IParkingLotsService, ParkingLotsService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

// Identity Configuration (Without Entity Framework)
builder.Services.AddScoped<IUserStore<ApplicationUser>, UserStore>();
builder.Services.AddScoped<IRoleStore<IdentityRole>, RoleStore>();

// Configure Identity with UserManager, SignInManager, etc.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddUserStore<UserStore>()
    .AddRoleStore<RoleStore>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
