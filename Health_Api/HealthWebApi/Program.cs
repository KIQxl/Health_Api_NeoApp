using Domain.Interfaces;
using Domain.Services;
using Entities.Dtos.PatientDto;
using Entities.Models;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddDbContext<HealthApiContext>();

builder.Services.AddScoped<IPatientServices, PatientServices>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//var config = new AutoMapper.MapperConfiguration(config =>
//{
//    config.CreateMap<CreatePatient, Patient>();
//    config.CreateMap<Patient, PatientView>();
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
