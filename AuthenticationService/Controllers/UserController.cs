using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILoggerApp _logger;
        public UserController(ILoggerApp logger)
        {
            _logger = logger;

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибки в программе");
        }

        [HttpGet]
        public User GetUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Denis",
                LastName = "Golubev",
                Login = "korchmar44",
                Email = "amigo36@mail.ru",
                Password = "Password"
            };
        }
    }
}
