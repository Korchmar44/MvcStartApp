using AuthenticationService.Models;
using AuthenticationService.Repositories.Interface;

namespace AuthenticationService.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> users;
        public UserRepository()
        {
            users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Denis",
                    LastName = "Golubev",
                    Login = "korchmar44",
                    Email = "amigo36@mail.ru",
                    Password = "Password"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Ivan",
                    LastName = "Golubev",
                    Login = "cocaine888",
                    Email = "amigo36@yandex.ru",
                    Password = "Password"},
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Charlie",
                    LastName = null,
                    Login = "example",
                    Email = "charlie@example.com",
                    Password = "Password"
                }
            };
        }
        // Метод для получения всех пользователей
        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User? GetByLogin(string login)
        {
            // Используем LINQ для поиска пользователя
            return users.FirstOrDefault(u => u.Login.Equals(login, StringComparison.OrdinalIgnoreCase));
        }
    }
}
