namespace TestApp.Models
{
    public class Course
    {
        public int ClasseId { get; set; }
        public int StudentId { get; set; }
        public Classe Classe { get; set; }
        public Student Student { get; set; }
    }
}
