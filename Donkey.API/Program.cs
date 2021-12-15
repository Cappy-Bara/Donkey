using Donkey.API.ClientDataProviders;
using Donkey.API.Settings.Authentication;
using Donkey.API.Settings.Swagger;
using Donkey.Core;
using Donkey.Core.Entities;
using Donkey.Core.Repositories;
using Donkey.Infrastructure.ErrorHandlingMiddleware;
using Donkey.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.AddAuthorizationFieldInSwagger());
builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();

builder.Services.AddCore();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUsersRepository, MockedUsersRepository>();

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

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
