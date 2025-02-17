using MvcStartApp.Models.Db;

namespace MvcStartApp.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
    }
}
