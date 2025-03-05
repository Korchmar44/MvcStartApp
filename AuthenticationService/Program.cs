using AuthenticationService;
using AuthenticationService.Interfaces;
using AuthenticationService.Repositories.Interface;
using AuthenticationService.Repositories.Repository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args); // Создание строителя веб-приложения

// Добавление сервисов в контейнер зависимостей
builder.Services.AddControllers(); // Добавление поддержки контроллеров
builder.Services.AddEndpointsApiExplorer(); // Добавление поддержки исследования конечных точек API
builder.Services.AddSwaggerGen(); // Добавление Swagger для генерации документации API
builder.Services.AddAuthentication(option => option.DefaultScheme = "Cookies") // Настройка схемы аутентификации
    .AddCookie("Cookies", option => // Добавление схемы аутентификации с использованием куки
    {
        option.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents // Настройка событий для аутентификации с куки
        {
            OnRedirectToLogin = redirectContext => // Определение поведения при перенаправлении на страницу входа
            {
                redirectContext.HttpContext.Response.StatusCode = 401; // Установка статуса ответа на 401 (Неавторизован)
                return Task.CompletedTask; // Завершение задачи
            }
        };
    });
builder.Services.AddSingleton<ILoggerApp, Logger>(); // Регистрация логгера как одиночки
builder.Services.AddSingleton<IUserRepository, UserRepository>(); // Регистрация репозитория пользователей как одиночки

// Настройка конфигурации маппинга с использованием AutoMapper
var mapperConfig = new MapperConfiguration((v) =>
{
    v.AddProfile(new MappingProfile()); // Добавление профиля маппинга
});
IMapper mapper = mapperConfig.CreateMapper(); // Создание экземпляра IMapper
builder.Services.AddSingleton(mapper); // Регистрация mappers как одиночки

var app = builder.Build(); // Создание веб-приложения на основе настроек

// Конфигурирование конвейера обработки HTTP-запросов
if (app.Environment.IsDevelopment()) // Проверка, является ли среда разработки
{
    app.UseDeveloperExceptionPage(); // Использование страницы с обработкой исключений для разработки
    app.UseSwagger(); // Включение Swagger
    app.UseSwaggerUI(); // Включение пользовательского интерфейса Swagger
}

app.UseRouting(); // Включение маршрутизации

app.UseAuthentication(); // Включение аутентификации

app.UseAuthorization(); // Включение авторизации

app.MapControllers(); // Настройка маршрутов для контроллеров

app.Run(); // Запуск приложения
