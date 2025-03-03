using MvcStartApp.Data.Models.Db;

namespace MvcStartApp.Repositories.Interfaces
{
    public interface ILogsRepository
    {
        Task CreateRequestLogAsync(Request request);
        Task<Request[]> GetRequests();
    }
}
