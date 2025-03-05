namespace AuthenticationService.Middleware
{
    public class LogMiddleware
    {
        private readonly ILogger<LogMiddleware> _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Проверка на null
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Получение IP-адреса клиента
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";

            // Логирование запроса с IP-адресом
            _logger.LogInformation("Handling request from IP: {IpAddress}, Method: {Method}, Path: {Path}", ipAddress, context.Request.Method, context.Request.Path);

            await _next(context); // Вызов следующего middleware

            // Дополнительное логирование после обработки запроса
            _logger.LogInformation("Finished handling request from IP: {IpAddress}", ipAddress);
        }
    }
}
