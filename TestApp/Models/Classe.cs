using System.ComponentModel.DataAnnotations;
namespace TestApp.Models
{
    public class Classe
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public School? School { get; set;}
        public ICollection<Course>? Courses { get; set; }
    }
}