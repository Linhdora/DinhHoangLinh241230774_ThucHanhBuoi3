using DhlLab6.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DhlLearner}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();