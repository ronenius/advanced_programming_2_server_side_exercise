using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using advanced_programming_2_server_side_exercise.Data;
using static Microsoft.Extensions.Hosting.IHost;
using advanced_programming_2_server_side_exercise.Services;
using advanced_programming_2_server_side_exercise.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<advanced_programming_2_server_side_exerciseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("advanced_programming_2_server_side_exerciseContext") ?? throw new InvalidOperationException("Connection string 'advanced_programming_2_server_side_exerciseContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTParams:Audience"],
        ValidIssuer = builder.Configuration["JWTParams:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTParams:SecretKey"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(x => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseCors();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reviews}/{action=Index}/{id?}");

app.MapHub<MyHub>("/myHub");

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MyHub>("/myHub");
});*/

app.Run();
