using dapperCRUD.Middleware;
using dapperCRUD.Services.CustomerService;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<GlobalMiddlewareInherited>();
//builder.Services.AddTransient<IMiddleware, GlobalMiddlewaer>();
//builder.Services.AddTransient<GlobalMiddlewaer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(); 

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<GlobalMiddlewaer>();
//app.UseMiddleware<GlobalMiddlewareImproved>();
//app.UseMiddleware<GlobalMiddlewareInherited>();

//app.Use(async (context, next) =>
//{
//    // Add code before request.

//    await next(context);

//    // Add code after request.
//});

app.MapControllers();

app.Run();
