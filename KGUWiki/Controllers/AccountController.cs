using KGUWiki.Models;
using KGUWiki.Models.Contexts;
using KGUWiki.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KGUWiki.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Email == loginViewModel.Email && x.Password == GetHashFormPassword(loginViewModel.Password));

                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Некорректные адрес электронной почты и(или) пароль");
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await Deauthenticate();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == registerViewModel.Email);

                if (user == null)
                {
                    user = _context.Users.FirstOrDefault(x => x.Name == registerViewModel.Name);

                    if (user == null)
                    {
                        var newUser = new User
                        {
                            Name = registerViewModel.Name,
                            Email = registerViewModel.Email,
                            Password = GetHashFormPassword(registerViewModel.Password),
                            RoleId = _context.Roles.FirstOrDefault(x => x.Name.Contains("Default User")).Id
                        };

                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();

                        await Authenticate(newUser);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Адрес электронной почты уже используется");
                }
            }

            return View(registerViewModel);
        }

        //Устанавка куки для аутентификации
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //Удаление куки для аутентификации
        private async Task Deauthenticate()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        //Получение хэша из пароля
        private string GetHashFormPassword(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
    }
}