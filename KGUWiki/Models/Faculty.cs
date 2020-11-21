using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KGUWiki.Models
{
    public class Faculty
    {
        public int Id { get; set; }

        [Display(Name = "Название факультета")]
        [Required(ErrorMessage = "Не указано полное название факультета")]
        public string FullName { get; set; } //Полное название факультета

        [Display(Name = "Аббревиатура факультета")]
        [Required(ErrorMessage = "Не указана аббревиатура факультета")]
        public string ShortName { get; set; } //Аббревиатура факультета

        public List<Department> Departments { get; set; } //Кафедры, связанные с факультетом
    }
}