using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KGUWiki.Models
{
    public class UniversityEmployee
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Не указано имя сотрудника")]
        public string FullName { get; set; } //ФИО

        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Не указана должность сотрудника")]
        public string Position { get; set; } //Должность

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; } //Фотография

        [Display(Name = "Эл. адрес")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        public string Email { get; set; } //Адрес электронной почты

        [Display(Name = "Номер телефона")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; } //Номер телефона

        [Display(Name = "Описание")]
        public string Description { get; set; } //Описание

        [Required(ErrorMessage = "Не указан факультет")]
        public int DepartmnetId { get; set; }
        public Department Department { get; set; } //Кафедра
    }
}
