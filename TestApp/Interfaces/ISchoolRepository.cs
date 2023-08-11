using TestApp.Models;

namespace TestApp.Interfaces
{
    public interface ISchoolRepository
    {
        ICollection<School> GetSchools();
        School GetSchoolById(int id);
        School GetSchoolByName(string name);
        long GetSchoolPhone(int id);
        string GetSchoolAddress(int id);
        ICollection<Student> GetSchoolStudents(int id);
        bool SchoolExists(int id);
        bool CreateSchool(School school);
        bool UpdateSchool(School school);
        bool DeleteSchool(School school);
        bool Save();
    }
}
