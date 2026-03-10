using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.BLL.Services;
using InventoryManagementSystem.DAL.Interfaces;
using InventoryManagementSystem.DAL.Models;
using InventoryManagementSystem.DAL.Repositories;
using InventoryManagementSystem.Managers;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IJwtTokenService, JwtTokenService>();

services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<IInventoryRepository, InventoryRepository>();
services.AddScoped<IInventoryUserRepository, InventoryUserRepository>();
services.AddScoped<IDictionaryRepository, DictionaryRepository>();
services.AddScoped<IInventoryFieldRepository, InventoryFieldRepository>();
services.AddScoped<IInventoryValueRepository, InventoryValueRepository>();
services.AddScoped<IInventoryCustomIdRepository, InventoryCustomIdRepository>();

services.AddScoped<IDictionaryService, DictionaryService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IInventoryService, InventoryService>();
services.AddScoped<IInventoryUserService, InventoryUserService>();
services.AddScoped<IInventoryFieldService, InventoryFieldService>();
services.AddScoped<IInventoryValueService, InventoryValueService>();
services.AddScoped<IInventoryCustomIdService, InventoryCustomIdService>();
services.AddScoped<IGeneratorService, GeneratorService>();

services.AddScoped<UserManager>();
services.AddScoped<InventoryManager>();
services.AddScoped<InventoryValueManager>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Добавление сервисов аутентификации
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
            };
        });

// Add services to the container.
services.AddControllersWithViews();
services.AddHttpContextAccessor();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
