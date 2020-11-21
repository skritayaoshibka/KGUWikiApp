using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KGUWiki.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Название кафедры")]
        [Required(ErrorMessage = "Не указано полное название кафедры")]
        public string FullName { get; set; } //Полное название кафедры

        [Display(Name = "Аббревиатура кафедры")]
        [Required(ErrorMessage = "Не указана аббревиатура кафедры")]
        public string ShortName { get; set; } //Аббревиатура кафедры

        public List<UniversityEmployee> UniversityEmployees { get; set; } //Сотрудники университета, связанные с кафедрой

        [Required(ErrorMessage = "Не указан факультет")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; } //Факультет
    }
}
