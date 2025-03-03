using Microsoft.AspNetCore.Mvc;
using MvcStartApp.Repositories.Interfaces;

namespace MvcStartApp.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogsRepository _repo;

        public LogsController(ILogsRepository repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var logs = await _repo.GetRequests();
            return View(logs);
        }
    }
}
