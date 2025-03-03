using Microsoft.EntityFrameworkCore;
using MvcStartApp.Data.Models.Db;

namespace MvcStartApp.Data
{
    public sealed class MvcStartAppContext : DbContext
    {
        /// Ссылка на таблицу Users
        public required DbSet<User> Users { get; set; }

        /// Ссылка на таблицу UserPosts
        public DbSet<UserPost>? UserPosts { get; set; }

        /// Ссылка на таблицу Requests
        public required DbSet<Request> Requests { get; set; }

        // Логика взаимодействия с таблицами в БД
        public MvcStartAppContext(DbContextOptions<MvcStartAppContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
