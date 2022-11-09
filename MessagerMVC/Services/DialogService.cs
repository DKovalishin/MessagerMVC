using MessagerMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagerMVC.Services
{
    public class DialogService
    {
        private UserDbContext context;

        public DialogService()
        {
            context = new UserDbContext();
        }

        public void AddDialog(Dialog dialog)
        {
            context.Dialogs.Add(dialog);
            context.SaveChanges();
        }

        public void CreateDialogAndAddToDb(User firstUser, User secondUser)
        {
            Dialog dialog = new Dialog(firstUser, secondUser);
            firstUser.Dialogs.Add(dialog);
            secondUser.Dialogs.Add(dialog);

            context.Dialogs.Add(dialog);
            context.Users.Update(firstUser);
            context.Users.Update(secondUser);
            context.SaveChanges();
        }

        public Dialog GetDialogIncludeUsersAndMessages(int dialogId)
        {
            Dialog dialog = context.Dialogs.Include(d => d.Users).Include(d => d.Messages).ThenInclude(m => m.UserSender).First(d => d.Id == dialogId);


            return dialog;
        }
    }
}
