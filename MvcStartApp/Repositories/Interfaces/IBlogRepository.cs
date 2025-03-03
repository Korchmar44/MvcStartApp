using MvcStartApp.Data.Models.Db;

namespace MvcStartApp.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
        Task<User[]> GetUsers();
    }
}
