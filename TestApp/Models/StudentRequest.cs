using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class StudentRequest
    {
        public string Name { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public ICollection<int> ClassesId { get; set; }
    }
}
