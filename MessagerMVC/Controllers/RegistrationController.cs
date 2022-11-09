using Microsoft.AspNetCore.Mvc;
using MessagerMVC.Models;
using MessagerMVC.Services;

namespace MessagerMVC.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult RegistrationPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrationPage(RegistrationUserModel userModel)
        {
            if(ModelState.IsValid && userModel != null)
            {
                var userService = new UserService();
                User user = userService.ConvertRegistrationUserModelToUser(userModel);

                switch (userService.IsUserNameExists(user.Name))
                {
                    case true: { ViewData["Error"] = "Пользователь с таким именем уже существует"; return View(); } break;
                    case false: break;
                    case null: { ViewData["Error"] = "Ошибка сервера, пользователь не был добавлен!"; return View(); } break;
                }

                userService.AddUserToDB(user);
                return RedirectToAction("RegistrationAcceptPage", userModel);
            }

            ViewData["Error"] = "Заполните все поля!";
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationAcceptPage(RegistrationUserModel user)
        {
            return View(user);
        }
    }
}
