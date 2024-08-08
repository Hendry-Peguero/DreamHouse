using DreamHouse.Core.Application.DependencyInjection;
using DreamHouse.Infrastructure.Identity.DependencyInjection;
using DreamHouse.Infrastructure.Persistence.DependencyInjection;
using DreamHouse.Infrastructure.Shared.DependencyInjection;
using WebApi.DreamHouse.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//api configuration
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

//dependency injections
builder.Services.AddPersistenceDependency(builder.Configuration);
builder.Services.AddApplicationDependency();
builder.Services.AddSharedDependency(builder.Configuration);
builder.Services.AddIdentityDependencyApi(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
}); ;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//seeds
await app.AddIdentitySeeds();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
