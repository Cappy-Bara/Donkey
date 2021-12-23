using Donkey.API.ClientDataProviders;
using Donkey.API.Settings.Authentication;
using Donkey.API.Settings.Swagger;
using Donkey.Core;
using Donkey.Core.Entities;
using FluentValidation.AspNetCore;
using Donkey.Infrastructure;
using Donkey.Infrastructure.ErrorHandlingMiddleware;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Donkey.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Donkey.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(x =>
    x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(gen => 
{
    gen.EnableAnnotations();
    gen.AddAuthorizationFieldInSwagger();
});

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
        );
});

var app = builder.Build();

app.EnableAutoMigrations();

app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
