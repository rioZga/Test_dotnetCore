using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public School? Schools { get; set; }
        public ICollection<Course>? Courses { get; set; }

    }
}