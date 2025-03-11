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
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false, // Désactive la validation de l'audience car l’API n'est pas enregistrée
        ValidateIssuer = true, // Vérifie que le token vient bien d'Azure Entra ID
        ValidIssuer = "https://login.microsoftonline.com/9c523e69-1868-4f28-826a-993ddf8f33a8/v2.0",
        ValidateLifetime = true, // Vérifie l'expiration du token
        ValidateIssuerSigningKey = true // Vérifie la signature du token
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"? Erreur d'authentification : {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("? Challenge JWT échoué !");
            return Task.CompletedTask;
        }
    };
});
//.AddJwtBearer(options =>
//{
//    options.Authority = "https://login.microsoftonline.com/9c523e69-1868-4f28-826a-993ddf8f33a8/v2.0";
//    options.Audience = "API_CLIENT_ID";
//    // options.TokenValidationParameters.ValidateAudience = false;
//    options.TokenValidationParameters.ValidIssuer = $"https://sts.windows.net/9c523e69-1868-4f28-826a-993ddf8f33a8/";
//});


builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(new string[] { "https://localhost:7118" })
        .WithHeaders(new string[] { "content-type" })
        .WithMethods(new string[] { "GET", "POST" });
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
