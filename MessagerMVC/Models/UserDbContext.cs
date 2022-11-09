using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using MessagerMVC.Services;

namespace MessagerMVC.Models
{
    public class User    
    {
        public int Id { get; set; }

        [UIHint("Name")]
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно содержать от {2} до {1} символов")]
        [Display(Name = "Имя:")]
        public string Name { get; set; }

        [UIHint("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от {2} до {1} символов")]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        public List<Dialog> Dialogs { get; set; }

        public User()
        {
            Dialogs = new List<Dialog>();
        }       
    }

    public class Dialog
    {
        public int Id { get; set; }
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }

        public Dialog()
        {
            Users= new List<User>();
            Messages= new List<Message>();
        }
        public Dialog(User user1, User user2)
        {
            Users = new List<User>();
            Messages = new List<Message>();
            Users.Add(user1);
            Users.Add(user2);
        }
    }

    public class Message:IMessage<string>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ThisDiaolgId { get; set; }
        public int UserSenderId { get; set; }
        public Dialog ThisDialog { get; set; }
        public User UserSender { get; set; }
        public DateTime Time { get; set; }

        public Message()
        {
            ThisDialog = new Dialog();  
        }
        public Message(Dialog dialog)
        {
            ThisDialog = dialog;
        }
    }

    public class UserDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MessagerMVC_DB;Trusted_Connection=true");
        }
    }
}
