using DhlLab5.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DhlSchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DhlSchoolContext")));
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Configuration.GetConnectionString("DhlSchoolContext") is not null)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DhlSchoolContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DhlStudent}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
