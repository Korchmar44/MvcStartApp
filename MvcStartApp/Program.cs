using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data;
using MvcStartApp.Middleware;
using MvcStartApp.Repositories;
using MvcStartApp.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
// ��������� �������� BlogContext � �������� ������� � ����������
builder.Services.AddDbContext<MvcStartAppContext>(option => option.UseSqlServer(connection));
// ����������� ������� ����������� ��� �������������� � ����� ������
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
// ����������� ������� ����������� ��� �������������� � ����� ������ (�����������)
builder.Services.AddScoped<ILogsRepository, LogsRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
