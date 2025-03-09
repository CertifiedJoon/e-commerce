using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// these are services.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt => 
{
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>(); //  <- lifetime is scoped to request. others include AddSingleton, AddTransient etc
var app = builder.Build();

// Configure the HTTP request pipeline.
// These are middleware.

app.MapControllers();
try
{
  using var scope = app.Services.CreateScope(); // <- to use services outside the scope of dep injection!
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<StoreContext>();
  await context.Database.MigrateAsync();
  await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
  Console.WriteLine(ex);
  throw;
}


app.Run();
