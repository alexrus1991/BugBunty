using BugBunty_Api.Infrastucture.ContextDB;
using BugBunty_Api.Services.BLL.IServices;
using BugBunty_Api.Services.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

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
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAD"));
builder.Services.AddAuthorization();

string BlazCors = "_BlazCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: BlazCors,
        policy =>
        {
            policy.WithHeaders(["Authorization"]);
            policy.WithOrigins(["https://localhost:7118"]);
        });
});


var app = builder.Build();



//app.UseSerilogRequestLogging();
app.UseCors(BlazCors);
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
