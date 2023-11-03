using CollectIt.Domain.Auth;
using CollectIt.Domain.Auth.Jwt;
using CollectIt.Domain.ConfigurationModel;
using CollectIt.Domain.Entity.Auth;
using GameCollector.API.ControllerMappings;
using GameCollector.Domain.ConfigurationModel;
using GameCollector.Domain.Game;
using GameCollector.Infra.Game;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var defaultSettingsSection = builder.Configuration.GetSection("DefaultMongoDbSettings");
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<DefaultSettings>(defaultSettingsSection);
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var productionConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"); 

var conectionString = productionConnectionString ?? defaultSettingsSection.GetSection("ConnectionString").Value;

builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
    (
        conectionString,
        defaultSettingsSection.GetSection("DatabaseName").Value
    )
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IGameItemRepository, GameItemRepository>();
builder.Services.AddScoped<IGameItemService, GameItemService>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("DefaultPolicy", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new()
    {
        ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JwtSettings:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();

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

app.ConfigureGameItemControllerMappings();
app.ConfigureAuthControllerMappings();

app.UseCors("DefaultPolicy");

app.Run();

