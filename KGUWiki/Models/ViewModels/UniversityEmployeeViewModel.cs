using Microsoft.AspNetCore.Http;

namespace KGUWiki.Models.ViewModels
{
    public class UniversityEmployeeViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }    //ФИО
        public string Position { get; set; }    //Должность
        public IFormFile Photo { get; set; }    //Файл (изображение), полученный из формы
        public string Email { get; set; }       //Адрес электронной почты
        public string PhoneNumber { get; set; } //Номер телефона
        public string Description { get; set; } //Описание
        public int DepartmentId { get; set; }   //Идентификатор кафедры
    }
}
