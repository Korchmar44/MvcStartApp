namespace CoreStartApp.Middleware
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(env.ContentRootPath, "Logs", "RequestLog.txt");
            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);

            // Передача запроса далее по конвейеру
            await next.Invoke(context);
        }
    }
}
