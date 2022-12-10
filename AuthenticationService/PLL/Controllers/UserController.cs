using AuthenticationService.BLL.Model.BL;
using AuthenticationService.BLL.Model.DL;
using AuthenticationService.BLL.Model.PL;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationService.PLL.Controllers
{
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        public UserController(
            ILogger logger,
            IMapper mapper,
            IUserRepository repo)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = repo;
        }

        [HttpGet]
        public User GetUser(string login)
        {
            return _userRepository.GetByLogin(login);
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel(string login)
        {
            var userViewModel = _mapper.Map<UserViewModel>(GetUser(login));

            return userViewModel;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) ||
                string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Логин или пароль не корректен");
            }
            User user = _userRepository.GetByLogin(login);
            if (user is null)
            {
                throw new AuthenticationException("Пользователь не найден");
            }
            if (user.Password != password)
            {
                throw new AuthenticationException("Введен неверный пароль");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.Name)
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
