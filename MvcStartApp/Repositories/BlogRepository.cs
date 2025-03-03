using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data;
using MvcStartApp.Data.Models.Db;
using MvcStartApp.Repositories.Interfaces;

namespace MvcStartApp.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        // ссылка на контекст
        private readonly MvcStartAppContext _context;

        // Метод-конструктор для инициализации
        public BlogRepository(MvcStartAppContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            // Проверка на null для user
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
            {
                await _context.Users.AddAsync(user);
            }

            // Сохранение изменений
            await _context.SaveChangesAsync();
        }

        public async Task<User[]> GetUsers()
        {
            var users = _context.Users;

            if (users == null)
            {
                // Здесь можно выбросить исключение или вернуть пустой массив в зависимости от бизнес-логики
                return Array.Empty<User>();
            }

            // Получаем всех пользователей
            return await users.ToArrayAsync();
        }
    }
}
