using CleanChat.Application.Repositories;
using CleanChat.Application.Services;
using CleanChat.Application.Services.Interface;
using CleanChat.Infrastructure;
using CleanChat.Infrastructure.context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Register configuration
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add database service
builder.Services.AddDbContext<ChatDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("ChatConnection"), 
    b => b.MigrationsAssembly("CleanChat.API")));

builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();

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

app.Run();
