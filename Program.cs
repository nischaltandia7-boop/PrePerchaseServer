using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PrePerchaseServer.Data;
using PrePerchaseServer.Models.amenities.repository;
using PrePerchaseServer.Models.amenities.service;
using PrePerchaseServer.Models.cities.repositories;
using PrePerchaseServer.Models.cities.service;
using PrePerchaseServer.Models.hotel.repository;
using PrePerchaseServer.Models.hotel.service;
using PrePerchaseServer.Models.hotelgroup.repository;
using PrePerchaseServer.Models.hotelgroup.service;
using PrePerchaseServer.Models.room_category.service;
using PrePerchaseServer.Models.roomcategory.repository;
using PrePerchaseServer.Models.roomcategory.service;
using PrePerchaseServer.Models.stay_highlight.repositories;
using PrePerchaseServer.Models.stay_highlight.service;
using Amazon.S3;
using PrePerchaseServer.Models.mediaBank;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IStayHighlightService, StayHighlightService>();
builder.Services.AddScoped<IStayHighlightRepository, StayHighlightRepository>();
builder.Services.AddScoped<IAmenitiesService, AmenitiesService>();
builder.Services.AddScoped<IAmenitiesRepository, AmenitiesRepository>();
builder.Services.AddScoped<IHotelGroupRepository,HotelGroupRepository>();
builder.Services.AddScoped<IHotelGroupService,HotelGroupService>();
builder.Services.AddScoped<IHotelRepository,HotelRepository>();
builder.Services.AddScoped<IHotelService,HotelService>();
builder.Services.AddScoped<IRoomCategoryRepository,RoomCategoryRepository>();
builder.Services.AddScoped<IRoomCategoryService,RoomCategoryService>();
builder.Services.AddScoped<IMediabankRepository,MediabankRepository>();
builder.Services.AddScoped<IMediabankService,MediabankService>();
builder.Services.AddScoped<StorageService>();


builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    return new AmazonS3Client(
        configuration["AWS:AccessKeyId"],
        configuration["AWS:SecretAccessKey"],
        Amazon.RegionEndpoint.APSouth1
    );
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();
app.Run();