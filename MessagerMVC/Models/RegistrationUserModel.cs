using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MessagerMVC.Models
{
    public class RegistrationUserModel
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
        [Compare("ConfirmPassword", ErrorMessage = "пароли не совпадают!")]
        public string Password { get; set; }

        [UIHint("ConfirmPassword")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заполните поле подтверждения пароля!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от {2} до {1} символов")]
        [Display(Name = "Подтверждение пароля:")]
        public string ConfirmPassword { get; set; }

        [UIHint("IsAgree")]
        [Required(ErrorMessage = "Это поле нужно заполнить!")]
        [Display(Name = "Пользовательское соглашение:")]
        public bool IsAgree { get; set; }
    }
}
