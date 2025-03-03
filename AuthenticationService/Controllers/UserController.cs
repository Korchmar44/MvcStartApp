using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private ILoggerApp _logger;
        public UserController(ILoggerApp logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

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

        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Denis",
                LastName = "Golubev",
                Login = "korchmar44",
                Email = "amigo36@mail.ru",
                Password = "Password"
            };

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
