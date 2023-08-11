using Microsoft.AspNetCore.Mvc;
using TestApp.Interfaces;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : Controller
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        [HttpGet]
        public IActionResult GetSchools() {
            var schools = _schoolRepository.GetSchools();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(schools);
        }

        [HttpGet("{id}")]
        public IActionResult GetSchoolById(int id)
        {
            if (!_schoolRepository.SchoolExists(id))
                return NotFound();
            var school = _schoolRepository.GetSchoolById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(school);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetSchoolByName([FromRoute] string name)
        {
            var school = _schoolRepository.GetSchoolByName(name);

            if (school == null)
                return NotFound();

            return Ok(school);
        }

        [HttpGet("{id}/address")]
        public IActionResult GetSchoolAddress(int id)
        {
            if (!_schoolRepository.SchoolExists(id))
                return NotFound();

            var address = _schoolRepository.GetSchoolAddress(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(address);

        }

        [HttpGet("{id}/phone")]
        public IActionResult GetSchoolPhone(int id)
        {
            if (!_schoolRepository.SchoolExists(id))
                return NotFound();

            var phone = _schoolRepository.GetSchoolPhone(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(phone);

        }

        [HttpGet("students/{id}")]
        public IActionResult GetSchoolStudents([FromRoute] int id)
        {
            if (!_schoolRepository.SchoolExists(id))
            {
                ModelState.AddModelError("", "School does not exist");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_schoolRepository.GetSchoolStudents(id));
        }

        [HttpPost]
        public IActionResult CreateSchool(School school)
        {
            if (school == null)
                return BadRequest(ModelState);
            var schoolExists = _schoolRepository.GetSchools()
                .Where(s => s.Name.Trim().ToUpper() == school.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (schoolExists != null)
            {
                ModelState.AddModelError("", "School already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newSchool = new School()
            {
                Name = school.Name,
                Adress = school.Adress,
                Phone = school.Phone,
            };

            if (!_schoolRepository.CreateSchool(newSchool))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            };
            return Ok("succefully created");
        }

        [HttpPut]
        public IActionResult UpdateSchool([FromBody] School school)
        {
            if (school == null)
                return BadRequest(ModelState);

            if (!_schoolRepository.SchoolExists(school.Id))
            {
                ModelState.AddModelError("", "School does not exist");
                return NotFound(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_schoolRepository.UpdateSchool(school))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchool([FromRoute]int id)
        {
            if (!_schoolRepository.SchoolExists(id))
            {
                ModelState.AddModelError("", "School does not exist");
                return NotFound(ModelState);
            }

            var school = _schoolRepository.GetSchoolById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_schoolRepository.DeleteSchool(school))
            {
                ModelState.AddModelError("", "Something went wrong!");
                return StatusCode(500, ModelState);
            }

            return Ok("Succefully deleted");
                
        }

    }
}
