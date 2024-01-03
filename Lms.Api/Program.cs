using Lms.Api.Services;
using Lms.Api.Installers;
using Lms.Auth.Installers;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.InstallDb(config);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.SetServices();
builder.Services.InstallJwt(config);
builder.Services.InstallSwagger();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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
