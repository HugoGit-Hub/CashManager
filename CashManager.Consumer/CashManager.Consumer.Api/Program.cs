using CashManager.Consumer.Api.Configuration;
using CashManager.Consumer.Application;
using CashManager.Consumer.Infrastructure;
using CashManager.Consumer.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(AssemblyMarker.Assembly));

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
