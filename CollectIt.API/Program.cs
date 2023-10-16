using GameCollector.API.ControllerMappings;
using GameCollector.Domain.ConfigurationModel;
using GameCollector.Domain.Game;
using GameCollector.Infra.Game;

var builder = WebApplication.CreateBuilder(args);  

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.Configure<DefaultSettings>
    (builder.Configuration.GetSection("DefaultSettings"));

builder.Services.AddScoped<IGameItemRepository, GameItemRepository>();
builder.Services.AddScoped<IGameItemService, GameItemService>();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureGameItemControllerMappings();

app.Run();
 
