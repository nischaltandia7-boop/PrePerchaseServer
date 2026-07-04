using System.Text.Json.Serialization;
using Amazon;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;

// Auth
using PrePerchaseServer.Modules.Auth;
using PrePerchaseServer.Modules.Auth.Repositories;
using PrePerchaseServer.Modules.Auth.Services;

// Existing Modules
using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.amenities.service;
using PrePerchaseServer.Models.cities.repositories;
using PrePerchaseServer.Models.cities.service;
using PrePerchaseServer.Models.hotel.repository;
using PrePerchaseServer.Models.hotel.service;
using PrePerchaseServer.Models.hotelgroup.repository;
using PrePerchaseServer.Models.hotelgroup.service;
using PrePerchaseServer.Models.mediaBank;
using PrePerchaseServer.Models.room_category.service;
using PrePerchaseServer.Models.roomcategory.repository;
using PrePerchaseServer.Models.roomcategory.service;
using PrePerchaseServer.Models.stay_highlight.repositories;
using PrePerchaseServer.Models.stay_highlight.service;
using PrePerchaseServer.Modules.Auth.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

#region Authentication

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

#endregion

#region AWS S3

builder.Services.AddSingleton<IAmazonS3>(_ =>
{
    return new AmazonS3Client(
        builder.Configuration["AWS:AccessKeyId"],
        builder.Configuration["AWS:SecretAccessKey"],
        RegionEndpoint.APSouth1);
});

#endregion

#region Repositories

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStayHighlightRepository, StayHighlightRepository>();
builder.Services.AddScoped<IAmenitiesRepository, AmenitiesRepository>();
builder.Services.AddScoped<IHotelGroupRepository, HotelGroupRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
builder.Services.AddScoped<IMediabankRepository, MediabankRepository>();

#endregion

#region Services

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IStayHighlightService, StayHighlightService>();
builder.Services.AddScoped<IAmenitiesService, AmenitiesService>();
builder.Services.AddScoped<IHotelGroupService, HotelGroupService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomCategoryService, RoomCategoryService>();
builder.Services.AddScoped<IMediabankService, MediabankService>();
builder.Services.AddScoped<StorageService>();

#endregion

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    await AuthSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();