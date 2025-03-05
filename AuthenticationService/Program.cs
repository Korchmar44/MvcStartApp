using AuthenticationService;
using AuthenticationService.Interfaces;
using AuthenticationService.Repositories.Interface;
using AuthenticationService.Repositories.Repository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args); // �������� ��������� ���-����������

// ���������� �������� � ��������� ������������
builder.Services.AddControllers(); // ���������� ��������� ������������
builder.Services.AddEndpointsApiExplorer(); // ���������� ��������� ������������ �������� ����� API
builder.Services.AddSwaggerGen(); // ���������� Swagger ��� ��������� ������������ API
builder.Services.AddAuthentication(option => option.DefaultScheme = "Cookies") // ��������� ����� ��������������
    .AddCookie("Cookies", option => // ���������� ����� �������������� � �������������� ����
    {
        option.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents // ��������� ������� ��� �������������� � ����
        {
            OnRedirectToLogin = redirectContext => // ����������� ��������� ��� ��������������� �� �������� �����
            {
                redirectContext.HttpContext.Response.StatusCode = 401; // ��������� ������� ������ �� 401 (�������������)
                return Task.CompletedTask; // ���������� ������
            }
        };
    });
builder.Services.AddSingleton<ILoggerApp, Logger>(); // ����������� ������� ��� ��������
builder.Services.AddSingleton<IUserRepository, UserRepository>(); // ����������� ����������� ������������� ��� ��������

// ��������� ������������ �������� � �������������� AutoMapper
var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile()); // ���������� ������� ��������
});
IMapper mapper = mapperConfig.CreateMapper(); // �������� ���������� IMapper
builder.Services.AddSingleton(mapper); // ����������� mappers ��� ��������

var app = builder.Build(); // �������� ���-���������� �� ������ ��������

// ���������������� ��������� ��������� HTTP-��������
if (app.Environment.IsDevelopment()) // ��������, �������� �� ����� ����������
{
    app.UseDeveloperExceptionPage(); // ������������� �������� � ���������� ���������� ��� ����������
    app.UseSwagger(); // ��������� Swagger
    app.UseSwaggerUI(); // ��������� ����������������� ���������� Swagger
}

app.UseRouting(); // ��������� �������������

app.UseAuthentication(); // ��������� ��������������

app.UseAuthorization(); // ��������� �����������

app.MapControllers(); // ��������� ��������� ��� ������������

app.Run(); // ������ ����������
