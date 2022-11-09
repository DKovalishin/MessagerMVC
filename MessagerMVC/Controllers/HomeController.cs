using Microsoft.AspNetCore.Mvc;
using MessagerMVC.Models;
using System.Diagnostics;
using MessagerMVC.Services;

namespace MessagerMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult HomePage()
        {
            var userService = new UserService();
            User user = userService.GetUserIncludeDialogs(HttpContext);
            return View(user);
        }

        public IActionResult SearchForUsersPage(string name)
        {
            var userService = new UserService();
            List<User> users = userService.GetUsersByName(name);
            return View(users);
        }

        public IActionResult CreateNewDialogPage(int secondUserId)
        {
            var userService = new UserService();
            var dialogService = new DialogService();
            User user = userService.GetUserIncludeDialogs(HttpContext);
            User? secondUser = userService.GetUserById(secondUserId);

            if(user.Id != secondUser.Id && !userService.AreUsersHaveDialog(user,secondUser))
            {
                Debug.WriteLine("AreUsersHaveDialog "+ Convert.ToString(userService.AreUsersHaveDialog(user, secondUser)));
                dialogService.CreateDialogAndAddToDb(user,secondUser);
                return View();
            }
            else if(user.Id == secondUser.Id)
            {
                ViewData["Error"] = "Нельзя создать диалог с самим собой!";
                return RedirectToAction("SearchForUsersPage", new { name = secondUser.Name });
            }
            Debug.WriteLine("45");
            ViewData["Error"] = "Диалог уже был создан!";
            return RedirectToAction("SearchForUsersPage", new { name = secondUser.Name });
        }
    }
}
