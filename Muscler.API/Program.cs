using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Muscler.API.ControllerMappings;
using Muscler.Domain.Auth;
using Muscler.Domain.Auth.Jwt;
using Muscler.Domain.ConfigurationModel;
using Muscler.Domain.Email;
using Muscler.Domain.Entity.Auth;
using SendGrid.Extensions.DependencyInjection;
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
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
    (
        Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? defaultSettingsSection.GetSection("ConnectionString").Value,
        defaultSettingsSection.GetSection("DatabaseName").Value
    )
    .AddDefaultTokenProviders();


builder.Services.AddSendGrid(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
});

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpContextAccessor();

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
        ValidateIssuer = true,
        ValidIssuer = Environment.GetEnvironmentVariable("DEFAULT_AUTH_ISSUER") ?? builder.Configuration.GetSection("JwtSettings:Issuer").Value,

        ValidateAudience = true,
        ValidAudience = Environment.GetEnvironmentVariable("DEFAULT_AUTH_AUDIENCE") ?? builder.Configuration.GetSection("JwtSettings:Audience").Value,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("DEFAULT_AUTH_SECRETKEY") ?? builder.Configuration.GetSection("JwtSettings:Key").Value)),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
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

app.ConfigureAuthControllerMappings();

app.UseCors("DefaultPolicy");

app.Run();

