using Microsoft.AspNetCore.Mvc;
using MessagerMVC.Models;
using MessagerMVC.Services;

namespace MessagerMVC.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult ChatPage(int dialogId)    
        { 
            User user = SessionService.GetUserFromSession(HttpContext);

            var dialogService = new DialogService();

            Dialog dialog = dialogService.GetDialogIncludeUsersAndMessages(dialogId);
            ViewBag.senderName = user.Name;
            ViewBag.dialogId = dialog.Id;

            return View(dialog);
        }
        
    }
}
