using Microsoft.AspNetCore.Http.HttpResults;
using TestApp.Data;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DataContext _context;

        public SchoolRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSchool(School school)
        {
            _context.Add(school);
            return Save();
        }

        public string GetSchoolAddress (int id)
        {
            var school =  _context.Schools.Find(id);
            if (school ==  null)
                return "" ;
            return school.Adress;
        }

        public School GetSchoolById(int id)
        {
            return _context.Schools.Where(s => s.Id == id).FirstOrDefault();
        }

        public School GetSchoolByName(string name)
        {
            return _context.Schools.Where(s => s.Name == name).FirstOrDefault();
        }

        public long GetSchoolPhone(int id)
        {
            var school = _context.Schools.Find(id);
            if (school == null ) return 0;
            return school.Phone;
        }

        public ICollection<School> GetSchools()
        {
            return _context.Schools.ToList();
        }

        public ICollection<Student> GetSchoolStudents(int id)
        {
            return _context.Students.Where(s => s.Schools.Id == id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool SchoolExists(int id)
        {
            return _context.Schools.Any(s => s.Id == id);
        }

        public bool UpdateSchool(School school)
        {
            _context.Update(school);
            return Save();
        }

        public bool DeleteSchool(School school)
        {
            _context.Schools.Remove(school);
            return Save();
        }
    }
}
