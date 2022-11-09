using MessagerMVC.Models;
using System.Text.Json;

namespace MessagerMVC.Services
{
    public static class SessionService
    {
        public static void AddUserToSession(User user, HttpContext context)
        {
            string serializedUser = JsonSerializer.Serialize(user);
            context.Session.SetString("user", serializedUser);
        }

        public static void DeleteUserFromSession(User user, HttpContext context)
        {
            context.Session.Remove("user");
        }

        public static User? GetUserFromSession(HttpContext context) 
        {
            return JsonSerializer.Deserialize<User?>(context.Session.GetString("user"));
        }
    }
}
