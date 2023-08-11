using TestApp.Models;

namespace TestApp.Interfaces
{
    public interface IClasseRepository
    {
        public ICollection<Classe> GetClasses();
        public ICollection<Classe> GetSchoolClasses(int schoolId);
        public Classe GetClasse(int id);
        public Classe GetClasseByTitle(string title);
        public bool CreateClasse(Classe classe);
        public bool UpdateClasse(Classe classe);
        public bool DeleteClasse(Classe classe);
        public bool ClasseExists(int id);
        public bool Save();

    }
}
