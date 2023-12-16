using Lms.Api.Db;
using Microsoft.EntityFrameworkCore;
using Lms.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var connString = config.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<DataContext>(options =>
        options.UseNpgsql(connString)
        .EnableSensitiveDataLogging()
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        )
    .AddLogging();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.SetServices();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsApi");

app.MapControllers();

app.Run();
