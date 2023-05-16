using CleanChat.Application.Repositories;
using CleanChat.Application.Services.Interface;
using CleanChat.Application.Services;
using CleanChat.Infrastructure;
using CleanChat.Web.Socket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddWebSocketManager();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseWebSockets();
app.MapWebSocketManager("/wss", app.Services.GetService<ChatMessageHandler>());
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
