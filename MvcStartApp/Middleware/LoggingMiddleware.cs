using MvcStartApp.Data.Models.Db;
using MvcStartApp.Repositories.Interfaces;

namespace MvcStartApp.Middleware
{
    public class LoggingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, ILogsRepository requestRepository)
        {
            var requestLog = new Request
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Url = context.Request.Path
            };
            await requestRepository.CreateRequestLogAsync(requestLog);

            // Передача запроса далее по конвейеру
            await next(context);
        }
    }
}
