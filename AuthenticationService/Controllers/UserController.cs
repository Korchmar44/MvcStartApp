using AuthenticationService.Interfaces;
using AuthenticationService.Middleware;
using AuthenticationService.Models;
using AuthenticationService.Repositories.Interface;
using AuthenticationService.Service.CustomException;
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
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        ILogger<LogMiddleware> _logger;
        private readonly IUserRepository _userRepository; // Добавим зависимость от IUserRepository
        public UserController(ILogger<LogMiddleware> logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel(string login)
        {
            var user = _userRepository.GetByLogin(login);

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
            // Проверка на пустые значения логина и пароля. Если одно из значений пустое, выбрасывается исключение.
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен"); // Исключение, если запрос некорректен

            // Получение объекта пользователя из репозитория по логину
            User? user = _userRepository.GetByLogin(login);

            // Проверка, найден ли пользователь. Если нет, выбрасываем исключение.
            if (user == null)
                throw new AuthenticationException("Пользователь не найден"); // Исключение, если пользователь не найден

            // Проверка, корректен ли введённый пароль
            if (!await _userRepository.ValidatePassword(user, password))
                throw new AuthenticationException("Введенный пароль не корректен"); // Исключение, если пароль некорректен

            // Создание списка утверждений (claims) для аутентификации
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login), // Добавление утверждения с логином пользователя
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRole.Name) // Добавление утверждения с ролью пользователя
            };

            // Создание объекта ClaimsIdentity с указанными утверждениями и схемой аутентификации
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie", // Название схемы аутентификации
                ClaimsIdentity.DefaultNameClaimType, // Тип имени
                ClaimsIdentity.DefaultRoleClaimType); // Тип роли

            // Подписание пользователя в системе с использованием куков
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            // Маппинг объекта пользователя на представление пользователя и возврат результата
            return _mapper.Map<UserViewModel>(user); // Возвращаем представление пользователя
        }
    }
}
