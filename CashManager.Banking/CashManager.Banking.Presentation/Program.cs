using CashManager.Banking.Application;
using CashManager.Banking.Infrastructure;
using CashManager.Banking.Infrastructure.Context;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using CashManager.Banking.Presentation.Configuration.Schemes;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CashManagerBankingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("M+ND73ZDpkzjè_GzdP%354DZok98e4z5d7f75f_çuzd")), //TODO: Replace this hard coded token key
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

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddMapster();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
