using System.ComponentModel.DataAnnotations;

namespace KGUWiki.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Не указан адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты")]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(5, ErrorMessage = "Длина пароля не может быть меньше пяти символов")]
        public string Password { get; set; }
    }
}