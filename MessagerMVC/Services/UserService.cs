using MessagerMVC.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace MessagerMVC.Services
{
    public class UserService
    {
        private UserDbContext context;

        public UserService()
        {
            context = new UserDbContext();
        }

        public void AddUserToDB(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User? GetUserById(int userId)
        {
            return context.Users.FirstOrDefault(u => u.Id == userId);                   
        }

        public bool? AuthenticationUser(User user)
        {
            try
            {
                User? userDB = (from u in context.Users
                                where u.Name == user.Name && u.Password == HashService.GetHash(user.Password)
                                select u).FirstOrDefault();

                if (userDB != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); return null; }
        }

        public User GetUserIncludeDialogs(HttpContext context)
        {
            User user = SessionService.GetUserFromSession(context);
            try
            {
                user = this.context.Users.Include(u => u.Dialogs).ThenInclude(d => d.Users).First(u => u.Id == user.Id);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return user;
        }

        public User GetUserByName(string name)
        {
            return context.Users.First(u => u.Name == name);
        }

        public bool? IsUserNameExists(string userName)
        {
            try
            {
                User? existUser = (from u in context.Users
                                   where u.Name == userName
                                   select u).FirstOrDefault();

                if (existUser == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); return null; }
        }

        public bool AreUsersHaveDialog(User firstUser, User secondUser)
        {
            Dialog? dialog = (from d in firstUser.Dialogs
                              where d.Users[0].Id == secondUser.Id || d.Users[1].Id == secondUser.Id
                              select d).FirstOrDefault();
            Debug.WriteLine(dialog.Id);
            if (dialog != null)
                return true;
            else
                return false;
        }

        public List<User> GetUsersByName(string name)
        {
            try
            {
                List<User> users = context.Users.Where(u => u.Name.Contains(name)).ToList();

                return users;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); return new List<User>(); }
        }

        public User ConvertRegistrationUserModelToUser(RegistrationUserModel userModel)
        {
            User user = new User();
            user.Name = userModel.Name;
            user.Password = HashService.GetHash(userModel.Password);

            return user;
        }
    }
}
