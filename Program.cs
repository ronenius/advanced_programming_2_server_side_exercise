using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using advanced_programming_2_server_side_exercise.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<advanced_programming_2_server_side_exerciseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("advanced_programming_2_server_side_exerciseContext") ?? throw new InvalidOperationException("Connection string 'advanced_programming_2_server_side_exerciseContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Index}/{id?}");

app.Run();
