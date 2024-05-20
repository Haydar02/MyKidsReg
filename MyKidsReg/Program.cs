using Microsoft.EntityFrameworkCore;
using MyKidsReg.Models;
using MyKidsReg.Repositories;
using MyKidsReg.Services;
using MyKidsReg.Services.CommunicationsServices;
using System;
using static MyKidsReg.Repositories.TeacherRelationsRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyKidsRegContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    //options.AddPolicy(name: "OnlyGET", policy =>
    //{
    //    policy.AllowAnyOrigin().WithMethods("GET").AllowAnyMethod();
    //});
});

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
builder.Services.AddScoped<IAdminRelationServices, AdminRelationServices>();
builder.Services.AddScoped<IAdminRelationsRepositories, AdminRelationsRepository>();
builder.Services.AddScoped<ITeacherRelationServices, TeacherRelationServices>();    
builder.Services.AddScoped<ITeacherRelationsRepositories, TeacherRelationRepository>();
builder.Services.AddScoped<IParentRelationServices, ParentRelationServices>();
builder.Services.AddScoped<IParentRelationsRepositories, ParentRelationRepository>();
builder.Services.AddScoped<IStudentLogServices, StudentLogServices>();
builder.Services.AddScoped<IStudentLogRepositories, StudentLogRepositories>();
builder.Services.AddScoped<IMessageRepositories,MessageRepositories>();
builder.Services.AddScoped<IMessageServices, MessageServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
