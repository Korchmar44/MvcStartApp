using AuthenticationService.Models;
using AuthenticationService.Repositories.Interface;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationService.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users;

        public UserRepository()
        {
                var roles = new Dictionary<string, Role>
            {
                { "Admin", new Role { Id = Guid.NewGuid(), Name = "Admin" } },
                { "User", new Role { Id = Guid.NewGuid(), Name = "User" } }
            };

                users = new List<User>
            {
                CreateUser("Denis", "Golubev", "korchmar44", "amigo36@mail.ru", "Password", roles["Admin"]),
                CreateUser("Ivan", "Golubev", "cocaine888", "amigo36@yandex.ru", "Cocaine888", roles["Admin"]),
                CreateUser("Charlie", null, "example", "charlie@example.com", "123", roles["User"])
            };
        }

        private User CreateUser(string firstName, string lastName, string login, string email, string password, Role userRole)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Login = login,
                Email = email,
                Password = HashPassword(password) , // Хешируем пароль
                UserRole = userRole
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create(); // Используем алгоритм SHA256
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashed = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashed); // Возвращаем хэш в виде строкового представления
        }

        // Метод для получения всех пользователей
        public IEnumerable<User> GetAll()
        {
            return users.AsReadOnly(); // Возвращаем только для чтения
        }

        public User? GetByLogin(string login)
        {
            // Используем LINQ для поиска пользователя с учетом регистра
            return users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }

        public Task<bool> ValidatePassword(User user, string password)
        {
            // Метод для проверки пароля
            var hashedPassword = HashPassword(password);
            return Task.FromResult(user.Password.Equals(hashedPassword));
        }
    }
}
