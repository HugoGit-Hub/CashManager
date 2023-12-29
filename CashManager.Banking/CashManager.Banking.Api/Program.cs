using CashManager.Banking.Api.Configuration;
using CashManager.Banking.Api.Configuration.Schemes;
using CashManager.Banking.Application;
using CashManager.Banking.Infrastructure;
using CashManager.Banking.Infrastructure.Context;
using CashManager.Banking.Presentation;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(AssemblyMarker.Assembly));

builder.Services.AddDbContext<CashManagerBankingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5001");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorization using Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fnkrjsfnrsjkfsrkfsrpdejfrsfkeopdqebrfsrdfoedifrsbdf")), //TODO: Replace this hard coded token key
            ValidIssuer = "CashManager.Banking", //TODO: Set this in config file
            ValidAudience = "CashManager" //TODO: Set this in config file
        };
    });

builder.Services
    .AddAuthentication("ApiKeyScheme")
    .AddScheme<ApiKeyOption, ApiKeyHandler>("ApiKeyScheme", options =>
    {
        options.HeaderField = "ApiKey";
        options.HeaderAttemptedValue = ",.PjqV#.|X>kgp{?JsygExquC;tVuf5%"; //TODO: Replace this hard coded api key
    });

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

builder.Services.AddMapster();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CashManagerBankingContext>();

    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }

    DataSeeder.Initialize(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
