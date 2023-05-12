using CleanChat.Application.Interfaces;
using CleanChat.Application.Services;
using CleanChat.Infrastructure.context;
using CleanChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//Register configuration
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add database service
builder.Services.AddDbContext<ChatDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("ChatConnection"), 
    b => b.MigrationsAssembly("CleanChat.API")));

builder.Services.AddScoped<ITopicRepository, TopicRepository>();
builder.Services.AddScoped<ITopicService, TopicService>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientServices, ClientServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
