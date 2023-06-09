using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;
using PatientMicroservice.Application.Interfaces;
using PatientMicroservice.Application.Repositories;
using Persistence.Context;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddCustomJwtAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IValidator<Patient>, PatientValidator>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler("/error");

app.UseMiddleware<ValidationExceptionHandlerMiddleware>();

app.Run();