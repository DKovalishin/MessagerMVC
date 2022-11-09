using MessagerMVC.Models;

namespace MessagerMVC.Services
{
    public class MessageService
    {
        private UserDbContext context;

        public MessageService()
        {
            context = new UserDbContext();   
        }

        public void AddMessageToDialog(Message message, Dialog dialog)
        {
            dialog.Messages.Add(message);

            context.Dialogs.Update(dialog);
            context.SaveChanges();
        }

        public async Task AddMessageAndSaveAsync(string content, string senderName, string dialogId)
        {
            await Task.Run(() =>
            {
                Dialog dialog = context.Dialogs.First(d => d.Id == Convert.ToInt32(dialogId));
                var userService = new UserService();
                User user = userService.GetUserByName(senderName);

                Message message = new Message { Content = content, UserSender = user, Time = DateTime.Now, ThisDialog = dialog };
                context.Messages.Add(message);
                context.Dialogs.Update(dialog);
                context.Users.Update(user);

                context.SaveChanges();
            });
        }
    }
}
