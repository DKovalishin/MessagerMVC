using Microsoft.AspNetCore.SignalR;
using MessagerMVC.Services;

namespace MessagerMVC.Hubs
{
    public class ChatHub:Hub
    {
        public async Task Send(string content, string senderName, string dialogId) //TODO переделай на чистую
        {
            var messageService = new MessageService();

            await messageService.AddMessageAndSaveAsync(content, senderName, dialogId);

            await this.Clients.All.SendAsync("Send", content, senderName, dialogId);
        }
    }
}
