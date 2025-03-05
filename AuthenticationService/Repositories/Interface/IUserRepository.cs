using AuthenticationService.Models;

namespace AuthenticationService.Repositories.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetByLogin(string login);
        Task<bool> ValidatePassword(User user, string password);
    }
}
