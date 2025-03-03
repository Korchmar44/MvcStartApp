using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data;
using MvcStartApp.Data.Models.Db;
using MvcStartApp.Repositories.Interfaces;

namespace MvcStartApp.Repositories
{
    public class LogsRepository: ILogsRepository
    {
        private readonly MvcStartAppContext _context;

        public LogsRepository(MvcStartAppContext context)
        {
            _context = context;
        }
        public async Task CreateRequestLogAsync(Request request)
        {
            //Добавление запроса
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение
            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            // Получим всех логов
            return await _context.Requests.ToArrayAsync();
        }
    }
}
