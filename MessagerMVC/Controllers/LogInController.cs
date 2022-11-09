using MessagerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MessagerMVC.Services;

namespace MessagerMVC.Controllers
{
    public class LogInController : Controller
    {
        private readonly ILogger<LogInController> _logger;

        public LogInController(ILogger<LogInController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult LogInPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogInPage(User user)
        {
            if(ModelState.IsValid && user != null)
            {
                var userService = new UserService();
                switch(userService.AuthenticationUser(user))
                {
                    case true: 
                        {
                            user = userService.GetUserByName(user.Name);
                            SessionService.AddUserToSession(user, HttpContext);
                            return RedirectToAction("HomePage","Home");
                        }
                    case false:
                        { 
                            ViewData["Error"] = "пользователь не найден";
                            return View();
                        } 
                    case null: 
                        { 
                            ViewData["Error"] = "Ошибка сервера!";
                            return View();
                        } 
                }               
            }
            else
                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}