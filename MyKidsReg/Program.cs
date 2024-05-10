using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using MyKidsReg.Repositories;
using MyKidsReg.Services;
using MyKidsReg.Services.CommunicationsServices;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyKidsRegContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddControllers()
//        .AddJsonOptions(options =>
//        {
//            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
//        });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepositories>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<CommunicationService>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<IStudentRepositores, StudentRepositories>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IDepartmentRepositories, DepartmentRepositories>();
builder.Services.AddScoped<IinstitutionServices, InstitutionServices>();
builder.Services.AddScoped<IinstitutionRepository, InstitutionRepositories>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();