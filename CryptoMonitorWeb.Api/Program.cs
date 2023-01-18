using System.Reflection;
using CryptoMonitorWeb.Data;
using CryptoMonitorWeb.Domain.Data;
using CryptoMonitorWeb.Domain.Queries;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICryptoDataProvider, CryptoDataProvider>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(AllCryptoTimeSeriesQuery).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader());

app.MapControllers();

app.Run();