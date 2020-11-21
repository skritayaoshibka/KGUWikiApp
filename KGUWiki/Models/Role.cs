using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KGUWiki.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя роли")]
        [Display(Name = "Название роли")]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}