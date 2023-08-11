using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Repository
{
    public class ClasseRepository : IClasseRepository
    {
        private readonly DataContext _context;

        public ClasseRepository(DataContext context)
        {
            _context  = context;
        }

        public ICollection<Classe> GetClasses()
        {
            return _context.Classes.ToList();
        }

        public ICollection<Classe> GetSchoolClasses(int schoolId)
        {
            return _context.Classes.Where(c => c.School.Id ==  schoolId).ToList();
        }

        public Classe GetClasse(int id)
        {
            return _context.Classes.Where(c => c.Id == id).Include(s => s.School).Include(s => s.Courses).FirstOrDefault();
        }

        public Classe GetClasseByTitle(string title)
        {
            return _context.Classes.Where(c => c.Title == title).FirstOrDefault();
        }

        public bool CreateClasse(Classe classe)
        {
            _context.Add(classe);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool ClasseExists(int id)
        {
            return _context.Classes.Any(c => c.Id == id);
        }

        public bool UpdateClasse(Classe classe)
        {
            _context.Update(classe);
            return Save();
        }

        public bool DeleteClasse(Classe classe)
        {
            _context.Classes.Remove(classe);
            return Save();
        }
    }
}
