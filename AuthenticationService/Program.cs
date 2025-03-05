using AuthenticationService;
using AuthenticationService.Interfaces;
using AuthenticationService.Repositories.Interface;
using AuthenticationService.Repositories.Repository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(option => option.DefaultScheme = "Cookies")
    .AddCookie("Cookies", option =>
    {
        option.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnRedirectToLogin = redirectContext =>
            {
                redirectContext.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSingleton<ILoggerApp, Logger>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var mapperConfig = new MapperConfiguration((v) =>
    {
        v.AddProfile(new MappingProfile());
    });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
