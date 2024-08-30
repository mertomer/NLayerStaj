using NLayerCore.Interfaces;  
using NLayerInfrastructure.MessageBroker;  
using NLayerService.Services;  
using NLayerRepository.Repositories;  
using NLayerRepository.DbContext;  
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Redis Baðlantýsýný DI'a Ekleme
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost"));  // Redis için localhost kullanýlýyormuþ

// RedisDbContext'i DI'a ekleme
builder.Services.AddScoped<RedisDbContext>();

// RabbitMQ Baðlantýsýný DI'a Ekleme
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory() { HostName = "localhost" };  // RabbitMQ için localhost kullanýlýyor
    return factory.CreateConnection();
});

// Servisleri DI'a Ekleme
builder.Services.AddScoped<ICurrencyService, CurrencyService>();  // ICurrencyService için implementasyon
builder.Services.AddScoped<IMessageBroker, RabbitMqBroker>();  // IMessageBroker için implementasyon
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();  // IExchangeRateService için implementasyon

// ICurrencyRepository için implementasyonu DI'a ekleme
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

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
