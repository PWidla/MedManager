using Domain.Entities;
using Domain.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IValidator<Prescription>, PrescriptionValidator>();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

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
app.UseExceptionHandler("/error");

app.UseMiddleware<ValidationExceptionHandlerMiddleware>();

app.Run();