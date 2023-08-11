using TestApp.Models;

namespace TestApp.Interfaces
{
    public interface IStudentRepository
    {
        public ICollection<Student> GetStudents();
        public Student GetStudent(int id);
        public Student GetStudentByName(string name);
        public string GetStudentName(int id);
        public bool StudentExists(int id);
        public bool CreateStudent(Student student, ICollection<int> classesId);
        public bool UpdateStudent(Student student);
        public bool DeleteStudent(Student student);
        public bool Save();
    }
}
