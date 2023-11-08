using CashManager.Banking.Application;
using CashManager.Banking.Infrastructure;
using CashManager.Banking.Infrastructure.Context;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CashManagerBankingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperTokenKey")), //TODO: Replace this hard coded token key
            ValidIssuer = "CashManager.Banking", //TODO: set this in config file
            ValidAudience = "CashManager" //TODO: set this in config file
        };
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

app.Use(async (context, next) =>
{
    var user = context.User;
    var nameIdentifierClaim = user.FindFirst(ClaimTypes.NameIdentifier);
    if (nameIdentifierClaim is null)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
        return;
    }

    if (user.Identity is ClaimsIdentity claimsIdentity)
    {
        claimsIdentity.AddClaim(nameIdentifierClaim);
    }

    await next.Invoke();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
