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

// Redis Ba�lant�s�n� DI'a Ekleme
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect("localhost"));  // Redis i�in localhost kullan�l�yormu�

// RedisDbContext'i DI'a ekleme
builder.Services.AddScoped<RedisDbContext>();

// RabbitMQ Ba�lant�s�n� DI'a Ekleme
builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory() { HostName = "localhost" };  // RabbitMQ i�in localhost kullan�l�yor
    return factory.CreateConnection();
});

// Servisleri DI'a Ekleme
builder.Services.AddScoped<ICurrencyService, CurrencyService>();  // ICurrencyService i�in implementasyon
builder.Services.AddScoped<IMessageBroker, RabbitMqBroker>();  // IMessageBroker i�in implementasyon
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();  // IExchangeRateService i�in implementasyon

// ICurrencyRepository i�in implementasyonu DI'a ekleme
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
