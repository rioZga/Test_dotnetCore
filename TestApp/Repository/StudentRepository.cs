using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context) 
        {
            _context = context;
        }
        public bool CreateStudent(Student student, ICollection<int> classesId)
        {
            foreach (int id in classesId)
            {
                var foundClasse = _context.Classes.Where(c => c.Id == id).FirstOrDefault();

                var newClasse = new Course()
                {
                    Student = student,
                    Classe = foundClasse,
                };

                _context.Courses.Add(newClasse);
            }
            _context.Students.Add(student);
            return Save();
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Where(s => s.Id == id)
                .Include(s => s.Courses)
                .Include(s => s.Schools)
                .FirstOrDefault();
        }

        public Student GetStudentByName(string name)
        {
            return _context.Students.FirstOrDefault(s => s.Name == name);
        }

        public string GetStudentName(int id)
        {
            var student = _context.Students.Where(s => s.Id == id).SingleOrDefault();
            if (student == null)
                return "";
            return student.Name;

        }

        public ICollection<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(s => s.Id == id);
        }

        public bool UpdateStudent(Student student)
        {
            _context.Update(student);
            return Save();
        }

        public bool DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            return Save();
        }
    }
}
