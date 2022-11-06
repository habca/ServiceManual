using System.Text.Json.Serialization;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;
using EtteplanMORE.ServiceManual.ApplicationCore.Interfaces;
using EtteplanMORE.ServiceManual.ApplicationCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IFactoryDeviceService<FactoryDevice>, FactoryDeviceService>();
builder.Services.AddScoped<IFactoryDeviceService<Maintenance>, MaintenanceService>();

builder.Services.AddSingleton<FactoryDeviceService>();
builder.Services.AddSingleton<MaintenanceService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Convert enum values into text in JSON.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // Use the property names as they are written.
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();