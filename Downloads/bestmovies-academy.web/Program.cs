using bestmovies_academy.web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

//Sätt upp database konfigurationn
builder.Services.AddDbContext<MoviesContext>(options=> 
options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

//Ladda data i database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<MoviesContext>();
    await context.Database.MigrateAsync();
    
    await SeedData.LoadData(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
