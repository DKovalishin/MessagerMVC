using MessagerMVC.Models;

namespace MessagerMVC.Models
{
    public interface IMessage<T>
    {
        public int Id { get; set; }
        public T Content { get; set; }
        public int ThisDiaolgId { get; set; }
        public int UserSenderId { get; set; }
        public Dialog ThisDialog { get; set; }
        public User UserSender { get; set; }
        public DateTime Time { get; set; }
    }

    //public interface IDialog<T>
    //{
    //    public int Id { get; set; }
    //    public List<IUser<T>> Users { get; set; }
    //    public List<IMessage<T>> Messages { get; set; }
    //}

    //public interface IUser<T>
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Password { get; set; }

    //    public List<IDialog<T>> Dialogs { get; set; }
    //}
}
