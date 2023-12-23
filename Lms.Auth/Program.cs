using Lms.Auth.Installers;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.InstallDb(config);
builder.Services.InstallSwagger();
builder.Services.InstallJwt(config);
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.SetServices();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

