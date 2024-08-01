using DreamHouse.Infrastructure.Identity.DependencyInjection;
using DreamHouse.Infrastructure.Persistence.DependencyInjection;
using DreamHouse.Core.Application.DependencyInjection;
using DreamHouse.Infrastructure.Shared.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddPersistenceDependency(builder.Configuration);
builder.Services.AddApplicationDependency();
builder.Services.AddSharedDependency(builder.Configuration);
builder.Services.AddIdentityDependency(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();
await app.AddIdentitySeeds();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Login}/{id?}");

app.Run();
