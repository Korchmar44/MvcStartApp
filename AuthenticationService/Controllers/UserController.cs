using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.Repositories.Interface;
using AuthenticationService.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private ILoggerApp _logger;
        private readonly IUserRepository _userRepository; // Добавим зависимость от IUserRepository
        public UserController(ILoggerApp logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибки в программе");
        }

        //[HttpGet]
        //public ActionResult<User> GetUser()
        //{
        //    // Используем репозиторий для получения пользователя, если необходимо
        //User? user = _userRepository.GetAll().FirstOrDefault(); // Пример получения пользоваетя из репозитория

        //    return user is not null ? Ok(user) : NotFound(); // Возвращаем 404, если пользователь не найден
        //}
        [Authorize]
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

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }

        [HttpGet("{login}")]
        public ActionResult<User> GetByLogin(string login)
        {
            var user = _userRepository.GetByLogin(login);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) ||String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User? user = _userRepository.GetByLogin(login);
            if (user == null)
                throw new AuthenticationException("Пользователь не найден");
            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
