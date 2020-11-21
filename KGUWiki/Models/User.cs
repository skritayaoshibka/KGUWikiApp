

using System.ComponentModel.DataAnnotations;

namespace KGUWiki.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Name { get; set; }

        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Не указан адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}