using BugBunty_Api.Infrastucture.ContextDB;
using BugBunty_Api.Services.BLL.IServices;
using BugBunty_Api.Services.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BugBuntyDbContext>(
   options =>
   {
       options.UseSqlServer(configuration.GetConnectionString("DEV"));
       // options.UseSqlServer(connectionString);
   });
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://login.microsoftonline.com/9c523e69-1868-4f28-826a-993ddf8f33a8/v2.0";
        //options.Audience = "BugBunty_Api";
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.ValidIssuer = $"https://sts.windows.net/9c523e69-1868-4f28-826a-993ddf8f33a8/";
    });

var app = builder.Build();

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
