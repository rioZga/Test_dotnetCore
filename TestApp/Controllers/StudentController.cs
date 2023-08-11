using Microsoft.AspNetCore.Mvc;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolRepository _schoolRepository;
        public StudentController(IStudentRepository studentRepository, ISchoolRepository schoolRepository, IClasseRepository classeRepository)
        {
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentRepository.GetStudents();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent([FromRoute] int id)
        {
            var student = _studentRepository.GetStudent(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetStudentByName([FromRoute] string name)
        {
            var student = _studentRepository.GetStudentByName(name);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("{id}/name")]
        public IActionResult GetStudentName([FromRoute] int id)
        {
            if (!_studentRepository.StudentExists(id))
                return NotFound();

            var name = _studentRepository.GetStudentName(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(name);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody]StudentRequest student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var school = _schoolRepository.GetSchoolById(student.SchoolId);

            if (school == null)
            {
                ModelState.AddModelError("", "School does not exist");
                return BadRequest(ModelState);
            }

            var newStudent = new Student()
            {
                Name = student.Name,
                Schools = school,
            };

            if (!_studentRepository.CreateStudent(newStudent, student.ClassesId))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully created");
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromBody]Student student)
        {
            if (student == null)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(student.Id))
            {
                ModelState.AddModelError("", "Student does not exist");
                return NotFound(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentRepository.UpdateStudent(student))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            if (!_studentRepository.StudentExists(id))
            {
                ModelState.AddModelError("", "Student does not exist");
                return NotFound(ModelState);
            }

            var student = _studentRepository.GetStudent(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentRepository.DeleteStudent(student))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully deleted");

        }
    }
}
